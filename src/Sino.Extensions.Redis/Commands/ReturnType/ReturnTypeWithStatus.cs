using System;
using System.Collections.Generic;
using System.Text;
using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// 返回类型为批量结果的命令对象
    /// </summary>
    public class ReturnTypeWithStatus : RedisCommand<string>
    {
        public bool IsEmpty { get; set; } = false;

        public bool IsNullable { get; set; } = false;

        public ReturnTypeWithStatus(string command, params object[] args)
            : base(command, args) { }

        public override string Parse(RedisReader reader)
        {
            if(IsEmpty)
            {
                RedisMessage type = reader.ReadType();
                if ((int)type == -1)
                    return string.Empty;
                else if (type == RedisMessage.Error)
                    throw new RedisException(reader.ReadStatus(false));

                throw new RedisProtocolException($"Unexpected type: {type}");
            }
            else if(IsNullable)
            {
                RedisMessage type = reader.ReadType();
                if (type == RedisMessage.Status)
                    return reader.ReadStatus(false);

                object[] result = reader.ReadMultiBulk(false);
                if (result != null)
                    throw new RedisProtocolException($"Expecting null MULTI BULK response. Received: {result.ToString()}");

                return null;
            }
            else
                return reader.ReadStatus();
        }
    }
}
