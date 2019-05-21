using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithString : CacheStoreCommand<string>
    {
        public bool IsNullable { get; set; } = false;

        public ResultWithString(string command, params object[] args)
            : base(command, args) { }

        public override string Parse(IBinaryReader reader)
        {
            if (IsNullable)
            {
                RedisMessage type = reader.ReadType();
                if (type == RedisMessage.Bulk)
                    return reader.ReadBulkString(false);
                reader.ReadMultiBulk(false);
                return null;
            }
            else
            {
                return reader.ReadBulkString();
            }
        }
    }
}
