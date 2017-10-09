using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis.Internal.Commands
{
    class RedisBytes : RedisCommand<byte[]>
    {
        public RedisBytes(string command, params object[] args)
            : base(command, args) { }

        public override byte[] Parse(RedisReader reader)
        {
            return reader.ReadBulkBytes(true);
        }
    }
}
