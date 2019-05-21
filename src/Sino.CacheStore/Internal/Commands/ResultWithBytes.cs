using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithBytes : CacheStoreCommand<byte[]>
    {
        public ResultWithBytes(string command, params object[] args)
            : base(command, args) { }

        public override byte[] Parse(IBinaryReader reader)
        {
            return reader.ReadBulkBytes(true);
        }
    }
}
