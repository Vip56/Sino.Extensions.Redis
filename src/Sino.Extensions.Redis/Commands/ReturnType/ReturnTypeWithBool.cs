using System;
using System.Collections.Generic;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为布尔结果的命令对象
    /// </summary>
    public class ReturnTypeWithBool : RedisCommand<bool>
    {
        public ReturnTypeWithBool(string command, params object[] args)
            : base(command, args) { }

        public override bool Parse(RedisReader reader)
        {
            return reader.ReadInt() == 1;
        }
    }
}
