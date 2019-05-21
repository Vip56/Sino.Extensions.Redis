using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithInt : CacheStoreCommand<long>
    {
        public ResultWithInt(string command, params object[] args)
            : base(command, args) { }

        public override long Parse(IBinaryReader reader)
        {
            return reader.ReadInt();
        }
    }
}
