using Sino.Extensions.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisConsole
{
    public class KeyTests
    {
        public IRedisCache Client { get; set; }

        public KeyTests(IRedisCache client)
        {
            Client = client;
        }

        public void Test()
        {
            Init();

            bool t = Client.Exists("tk1");
            t = Client.Exists("tk100");

            t = Client.Expire("tk1", 500);
            t = Client.PExpire("tk1", 500 * 1000);

            t = Client.Expire("tk100", 500);
            t = Client.PExpire("tk100", 500 * 1000);

            Destroy();
        }

        public async Task TestAsync()
        {
            Init();

            bool t = await Client.ExistsAsync("tk1");
            t = await Client.ExistsAsync("tk100");

            t = await Client.ExpireAsync("tk2", 500);
            t = await Client.PExpireAsync("tk2", 500 * 1000);

            t = await Client.ExpireAsync("tk100", 500);
            t = await Client.PExpireAsync("tk100", 500 * 1000);

            Destroy();
        }

        public void Init()
        {
            for(int i = 0; i < 100; i++)
            {
                Client.Set("tk" + i, i.ToString());
            }
        }

        public void Destroy()
        {
            for(int i = 0; i < 100; i++)
            {
                Client.Remove("tk" + i);
            }
        }
    }
}
