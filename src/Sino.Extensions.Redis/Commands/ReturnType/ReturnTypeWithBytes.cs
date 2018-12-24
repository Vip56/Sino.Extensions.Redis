using System;
using System.Collections.Generic;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为字节数组的命令对象
    /// </summary>
    public class ReturnTypeWithBytes : RedisCommand<byte[]>
    {
        public ReturnTypeWithBytes(string command, params object[] args)
            : base(command, args) { }

        public override byte[] Parse(RedisReader reader)
        {
            return reader.ReadBulkBytes(true);
        }
    }
}
