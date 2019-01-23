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
                Host = "192.168.1.235",
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

            //var str = new StringTests(client);
            //str.Test();
            //str.TestAsync().Wait();

            var load = new LoadTest(client);
            //load.Test();
            //load.TestAsync().Wait();
            load.TestParallel();

            Console.ReadLine();
        }
    }
}
