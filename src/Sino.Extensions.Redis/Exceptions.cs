using System;

namespace Sino.Extensions.Redis
{
    public class RedisClientException : Exception
    {
        public RedisClientException(string message)
            : base(message)
        { }

        public RedisClientException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class RedisProtocolException : RedisClientException
    {
        public RedisProtocolException(string message)
            : base(message)
        { }
    }

    public class RedisException : RedisClientException
    {
        public RedisException(string message)
            : base(message)
        { }
    }
}
