using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Configuration
{
    public class RedisCacheStoreOptions
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public string InstanceName { get; set; }
    }
}
