using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithFloat : CacheStoreCommand<double?>
    {
        public ResultWithFloat(string command, params object[] args)
            : base(command, args) { }

        public override double? Parse(IBinaryReader reader)
        {
            string result = reader.ReadBulkString();
            if (result == null)
                return null;
            return double.Parse(result, NumberStyles.Float, CultureInfo.InvariantCulture);
        }
    }
}
