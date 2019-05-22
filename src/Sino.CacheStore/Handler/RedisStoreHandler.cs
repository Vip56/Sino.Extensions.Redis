using Sino.CacheStore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public class RedisStoreHandler : StoreHandler
    {
        public Encoding Encoding { get; set; }

        public IStorePipeline Pipeline { get; set; }

        public RedisStoreHandler(IStorePipeline pipeline)
        {
            Encoding = new UTF8Encoding(false);
            Pipeline = pipeline;
        }

        public override async Task<CacheStoreCommand<T>> Process<T>(CacheStoreCommand<T> command)
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
                default:
                    {
                        throw new CacheStoreException("No Support Command");
                    }
                    break;
            }
        }


    }
}
