using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为浮点数结果的命令对象
    /// </summary>
    public class ReturnTypeWithFloat : RedisCommand<double?>
    {
        public ReturnTypeWithFloat(string command, params object[] args)
            : base(command, args) { }

        public override double? Parse(RedisReader reader)
        {
            string result = reader.ReadBulkString();
            if (result == null)
                return null;
            return double.Parse(result, NumberStyles.Float, CultureInfo.InvariantCulture);
        }
    }
}
