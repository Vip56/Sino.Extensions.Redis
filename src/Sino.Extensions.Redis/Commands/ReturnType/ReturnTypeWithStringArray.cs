using System;
using System.Collections.Generic;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为字符串组结果的命令对象
    /// </summary>
    public class ReturnTypeWithStringArray : RedisCommand<string[]>
    {
        readonly ReturnTypeWithString _memberCommand;

        public ReturnTypeWithStringArray(string command, params object[] args)
            : base(command, args)
        {
            _memberCommand = new ReturnTypeWithString(command, args);
        }

        public override string[] Parse(RedisReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            long count = reader.ReadInt(false);
            return Read(count, reader);
        }

        protected virtual string[] Read(long count, RedisReader reader)
        {
            string[] array = new string[count];
            for (int i = 0; i < array.Length; i++)
                array[i] = _memberCommand.Parse(reader);
            return array;
        }
    }
}
