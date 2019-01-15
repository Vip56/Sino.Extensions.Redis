using Sino.Extensions.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisConsole
{
    public class HashTests
    {
        public IRedisCache Client { get; set; }

        public HashTests(IRedisCache client)
        {
            Client = client;
        }

        public void Test()
        {
            Init();

            bool t = Client.HExists("th", "f0");
            t = Client.HExists("th", "f100");
            t = Client.HExists("th1", "f0");

            string r = Client.HGet("th", "f0");
            r = Client.HGet("th", "f100");
            r = Client.HGet("th1", "f0");

            long l = Client.HLen("th");
            l = Client.HLen("th0");

            t = Client.HSetNx("th", "f0", "123");
            t = Client.HSetNx("th0", "f0", "123");

            Client.Remove("th0");

            Destroy();
        }

        public async Task TestAsync()
        {
            Init();

            bool t = await Client.HExistsAsync("th", "f0");
            t = await Client.HExistsAsync("th", "f100");
            t = await Client.HExistsAsync("th1", "f0");

            string r = await Client.HGetAsync("th", "f0");
            r = await Client.HGetAsync("th", "f100");
            r = await Client.HGetAsync("th1", "f0");

            long l = await Client.HLenAsync("th");
            l = await Client.HLenAsync("th0");

            t = await Client.HSetNxAsync("th", "f0", "123");
            t = await Client.HSetNxAsync("th0", "f0", "123");

            Client.Remove("th0");

            Destroy();
        }

        public void Init()
        {
            for(int i = 0; i < 100; i++)
            {
                Client.HSet("th", "f" + i, i.ToString());
            }
        }

        public void Destroy()
        {
            for(int i = 0; i < 100; i++)
            {
                Client.HDel("th", "f" + i);
            }
        }
    }
}
