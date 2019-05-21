using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithBool : CacheStoreCommand<bool>
    {
        public ResultWithBool(string command, params object[] args)
            : base(command, args) { }

        public override bool Parse(IBinaryReader reader)
        {
            return reader.ReadInt() == 1;
        }
    }
}
