using System;
using System.Collections.Generic;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为字符串的命令对象
    /// </summary>
    public class ReturnTypeWithString : RedisCommand<string>
    {
        public bool IsNullable { get; set; } = false;

        public ReturnTypeWithString(string command, params object[] args)
            : base(command, args) { }

        public override string Parse(RedisReader reader)
        {
            if (IsNullable)
            {
                RedisMessage type = reader.ReadType();
                if (type == RedisMessage.Bulk)
                    return reader.ReadBulkString(false);
                reader.ReadMultiBulk(false);
                return null;
            }
            else
            {
                return reader.ReadBulkString();
            }
        }
    }
}
