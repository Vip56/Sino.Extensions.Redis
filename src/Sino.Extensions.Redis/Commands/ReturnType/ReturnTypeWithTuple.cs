using System;
using System.Collections.Generic;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为元组结果的命令对象
    /// </summary>
    public class ReturnTypeWithTuple : RedisCommand<Tuple<string, string>>
    {
        public ReturnTypeWithTuple(string command, params object[] args)
            : base(command, args) { }

        public override Tuple<string, string> Parse(RedisReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            reader.ExpectSize(2);
            return Tuple.Create(reader.ReadBulkString(), reader.ReadBulkString());
        }
    }
}
