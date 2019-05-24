using Microsoft.Extensions.Options;
using Sino.CacheStore.Configuration;
using Sino.CacheStore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public class RedisCacheStoreHandler : CacheStoreHandler
    {
        private readonly RedisCacheStoreOptions _options;
        private readonly string _instance;
        private readonly string _password;

        public Encoding Encoding { get; set; }

        public ICacheStorePipeline Pipeline { get; set; }

        public RedisCacheStoreHandler(IOptions<CacheStoreOptions> optionsAccessor)
            : this(optionsAccessor?.Value) { }

        public RedisCacheStoreHandler(CacheStoreOptions options, CacheStorePipeline pipeline = null)
        {
            _options = options.Redis ?? throw new ArgumentNullException(nameof(options.Redis));

            _instance = options.Redis.InstanceName ?? string.Empty;

            if (string.IsNullOrEmpty(_options.Host))
                throw new ArgumentNullException(nameof(_options.Host));

            _password = _options.Password;

            Encoding = new UTF8Encoding(false);
            Pipeline = pipeline ?? new RedisCacheStorePipeline(_options.Host, _options.Port);
        }

        public override async Task Init()
        {
            if (!string.IsNullOrEmpty(_password))
            {
                var cmd = new StatusCommand("AUTH", _password);
                await ProcessAsync(cmd);
            }
        }

        public override async Task<CacheStoreCommand<T>> ProcessAsync<T>(CacheStoreCommand<T> command)
        {
            if (!Pipeline.IsConnected)
                await Pipeline.ConnectAsync();
            var rbw = new RedisBinaryWriter(Encoding);
            var writeBytes = rbw.Prepare(command);

            var readBytes = await Pipeline.SendAsnyc(writeBytes);

            using (var ms = new MemoryStream(readBytes))
            {
                SetResult(command, ms);
            }
            return command;
        }

        private void SetResult<T>(CacheStoreCommand<T> command, Stream stream)
        {
            var reader = new RedisBinaryReader(stream, Encoding);
            switch(command)
            {
                case BoolCommand boolcmd:
                    {
                        boolcmd.Result = reader.ReadInt() == 1;
                    }
                    break;
                case IntCommand intcmd:
                    {
                        intcmd.Result = reader.ReadInt();
                    }
                    break;
                case StatusCommand scmd:
                    {
                        if (scmd.IsEmpty)
                        {
                            RedisMessage type = reader.ReadType();
                            if ((int)type == -1)
                                scmd.Result = string.Empty;
                            else if (type == RedisMessage.Error)
                                throw new CacheStoreException(reader.ReadStatus(false));

                            throw new CacheStoreProtocolException($"Unexpected type: {type}");
                        }
                        else if(scmd.IsNullable)
                        {
                            RedisMessage type = reader.ReadType();
                            if (type == RedisMessage.Status)
                                scmd.Result = reader.ReadStatus(false);

                            object[] result = reader.ReadMultiBulk(false);
                            if (result != null)
                                throw new CacheStoreProtocolException($"Expecting null MULTI BULK response. Received: {result.ToString()}");

                            scmd.Result = null;
                        }
                        else
                        {
                            scmd.Result = reader.ReadStatus();
                        }
                    }
                    break;
                case StringCommand stringcmd:
                    {
                        if (stringcmd.IsNullable)
                        {
                            RedisMessage type = reader.ReadType();
                            if (type == RedisMessage.Bulk)
                                stringcmd.Result = reader.ReadBulkString(false);
                            reader.ReadMultiBulk(false);
                            stringcmd.Result = null;
                        }
                        else
                        {
                            stringcmd.Result = reader.ReadBulkString();
                        }
                    }
                    break;
                case TupleCommand tuplecmd:
                    {
                        reader.ExpectType(RedisMessage.MultiBulk);
                        reader.ExpectSize(2);
                        tuplecmd.Result = Tuple.Create(reader.ReadBulkString(), reader.ReadBulkString());
                    }
                    break;
                case BytesCommand bytescmd:
                    {
                        bytescmd.Result = reader.ReadBulkBytes(true);
                    }
                    break;
                default:
                    {
                        throw new CacheStoreException("No Support Command");
                    }
                    break;
            }
        }


    }
}
