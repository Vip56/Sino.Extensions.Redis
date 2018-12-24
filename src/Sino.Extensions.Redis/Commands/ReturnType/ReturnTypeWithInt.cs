using System;
using System.Collections.Generic;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为数值结果的命令对象
    /// </summary>
    public class ReturnTypeWithInt : RedisCommand<long>
    {
        public ReturnTypeWithInt(string command, params object[] args)
            : base(command, args) { }

        public override long Parse(RedisReader reader)
        {
            return reader.ReadInt();
        }
    }
}
