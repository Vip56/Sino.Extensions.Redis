using System;

namespace Sino.Extensions.Redis
{
    public class RedisSubscriptionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The subscription response
        /// </summary>
        public RedisSubscriptionChannel Response { get; private set; }

        internal RedisSubscriptionChangedEventArgs(RedisSubscriptionChannel response)
        {
            Response = response;
        }
    }
}
