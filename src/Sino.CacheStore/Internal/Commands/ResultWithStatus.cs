using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithStatus : CacheStoreCommand<string>
    {
        public bool IsEmpty { get; set; } = false;

        public bool IsNullable { get; set; } = false;

        public ResultWithStatus(string command, params object[] args)
            : base(command, args) { }

        public override string Parse(IBinaryReader reader)
        {
            if (IsEmpty)
            {
                RedisMessage type = reader.ReadType();
                if ((int)type == -1)
                    return string.Empty;
                else if (type == RedisMessage.Error)
                    throw new CacheStoreException(reader.ReadStatus(false));

                throw new CacheStoreProtocolException($"Unexpected type: {type}");
            }
            else if (IsNullable)
            {
                RedisMessage type = reader.ReadType();
                if (type == RedisMessage.Status)
                    return reader.ReadStatus(false);

                object[] result = reader.ReadMultiBulk(false);
                if (result != null)
                    throw new CacheStoreProtocolException($"Expecting null MULTI BULK response. Received: {result.ToString()}");

                return null;
            }
            else
                return reader.ReadStatus();
        }
    }
}
