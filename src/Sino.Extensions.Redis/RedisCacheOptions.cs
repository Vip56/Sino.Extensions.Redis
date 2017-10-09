using Microsoft.Extensions.Options;

namespace Sino.Extensions.Redis
{
    public class RedisCacheOptions : IOptions<RedisCacheOptions>
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public string InstanceName { get; set; }

        RedisCacheOptions IOptions<RedisCacheOptions>.Value
        {
            get { return this; }
        }
    }
}
