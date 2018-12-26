using Sino.Extensions.Redis.Internal.IO;

namespace Sino.Extensions.Redis
{
    public class RedisCommand
    {
        readonly string _command;
        readonly object[] _args;

        public string Command { get { return _command; } }

        public object[] Arguments { get { return _args; } }

        protected RedisCommand(string command, params object[] args)
        {
            _command = command;
            _args = args;
        }
    }

    public abstract class RedisCommand<T> : RedisCommand
    {
        protected RedisCommand(string command, params object[] args)
            : base(command, args) { }

        public abstract T Parse(RedisReader reader);

        public override string ToString()
        {
            return $"{Command} {string.Join(" ", Arguments)}";
        }
    }
}
