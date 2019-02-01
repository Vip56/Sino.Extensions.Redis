using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Configuration
{
    public class CacheStoreWithRedis
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }
    }
}
