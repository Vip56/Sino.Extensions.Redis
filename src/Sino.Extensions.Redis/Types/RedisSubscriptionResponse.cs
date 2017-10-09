namespace Sino.Extensions.Redis
{
    public class RedisSubscriptionResponse
    {
        readonly string _channel;
        readonly string _pattern;
        readonly string _type;

        /// <summary>
        /// Get the subscription channel name
        /// </summary>
        public string Channel { get { return _channel; } }

        /// <summary>
        /// Get the subscription pattern
        /// </summary>
        public string Pattern { get { return _pattern; } }

        /// <summary>
        /// Get the message type
        /// </summary>
        public string Type { get { return _type; } }

        internal RedisSubscriptionResponse(string type, string channel, string pattern)
        {
            _type = type;
            _channel = channel;
            _pattern = pattern;
        }
    }

    /// <summary>
    /// Represents a Redis subscription channel
    /// </summary>
    public class RedisSubscriptionChannel : RedisSubscriptionResponse
    {
        readonly long _count;

        /// <summary>
        /// Get the count of active subscriptions
        /// </summary>
        public long Count { get { return _count; } }

        internal RedisSubscriptionChannel(string type, string channel, string pattern, long count)
            : base(type, channel, pattern)
        {
            _count = count;
        }
    }

    /// <summary>
    /// Represents a Redis subscription message
    /// </summary>
    public class RedisSubscriptionMessage : RedisSubscriptionResponse
    {
        readonly string _body;

        /// <summary>
        /// Get the subscription message
        /// </summary>
        public string Body { get { return _body; } }

        internal RedisSubscriptionMessage(string type, string channel, string body)
            : base(type, channel, null)
        {
            _body = body;
        }

        internal RedisSubscriptionMessage(string type, string pattern, string channel, string body)
            : base(type, channel, pattern)
        {
            _body = body;
        }
    }
}
