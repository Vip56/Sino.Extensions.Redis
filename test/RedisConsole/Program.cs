using Sino.Extensions.Redis;
using System;

namespace RedisConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RedisCache(new RedisCacheOptions
            {
                Host = "127.0.0.1",
                Port = 6379,
                InstanceName = "console_"
            });

            //var key = new KeyTests(client);
            //key.Test();
            //key.TestAsync().Wait();

            //var hash = new HashTests(client);
            //hash.Test();
            //hash.TestAsync().Wait();

            //var list = new ListTests(client);
            //list.Test();
            //list.TestAsync().Wait();

            var str = new StringTests(client);
            //str.Test();
            str.TestAsync().Wait();
        }
    }
}
