using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithTuple : CacheStoreCommand<Tuple<string, string>>
    {
        public ResultWithTuple(string command, params object[] args)
            : base(command, args) { }

        public override Tuple<string, string> Parse(IBinaryReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            reader.ExpectSize(2);
            return Tuple.Create(reader.ReadBulkString(), reader.ReadBulkString());
        }
    }
}
