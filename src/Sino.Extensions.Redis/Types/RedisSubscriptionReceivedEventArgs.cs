using System;

namespace Sino.Extensions.Redis
{
    public class RedisSubscriptionReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// The subscription message
        /// </summary>
        public RedisSubscriptionMessage Message { get; private set; }

        internal RedisSubscriptionReceivedEventArgs(RedisSubscriptionMessage message)
        {
            Message = message;
        }
    }
}
