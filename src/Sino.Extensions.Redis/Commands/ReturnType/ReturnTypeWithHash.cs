using System;
using System.Collections.Generic;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为Hash字典结果的命令对象
    /// </summary>
    public class ReturnTypeWithHash : RedisCommand<Dictionary<string, string>>
    {
        public ReturnTypeWithHash(string command, params object[] args)
            : base(command, args) { }

        public override Dictionary<string, string> Parse(RedisReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            long count = reader.ReadInt(false);
            var dict = new Dictionary<string, string>();
            string key = string.Empty;
            for (int i = 0; i < count; i++)
            {
                if (i % 2 == 0)
                    key = reader.ReadBulkString();
                else
                    dict[key] = reader.ReadBulkString();
            }
            return dict;
        }
    }
}
