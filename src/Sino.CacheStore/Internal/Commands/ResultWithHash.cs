using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithHash : CacheStoreCommand<Dictionary<string ,string>>
    {
        public ResultWithHash(string command, params object[] args)
            :base(command, args) { }

        public override Dictionary<string, string> Parse(IBinaryReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            long count = reader.ReadInt(false);
            var dict = new Dictionary<string, string>();
            string key = string.Empty;
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                    key = reader.ReadBulkString();
                else
                    dict[key] = reader.ReadBulkString();
            }
            return dict;
        }
    }
}
