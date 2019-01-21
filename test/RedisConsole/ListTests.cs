using Sino.Extensions.Redis;
using System.Threading.Tasks;

namespace RedisConsole
{
    public class ListTests
    {
        public IRedisCache Client { get; set; }

        public ListTests(IRedisCache client)
        {
            Client = client;
        }

        public void Test()
        {
            Init();

            long l = Client.LLen("tl");
            l = Client.LLen("tl1");

            string r = Client.LIndex("tl", 0);
            r = Client.LIndex("tl", 101);

            var item = Client.BLPop(10, "tl");
            item = Client.BLPop(10, "tl1");

            item = Client.BRPop(10, "tl");
            item = Client.BRPop(10, "tl1");

            r = Client.BRPopLPush("tl", "sl", 10);
            r = Client.BRPopLPush("tl1", "sl1", 10);

            l = Client.LPush("tl2", "test");
            r = Client.LPop("tl2");

            l = Client.LPushX("tl2", "test");
            l = Client.LPushX("tl", "test");
            Client.LPop("tl");

            l = Client.LRem("tl", 0, "1");

            l = Client.RPush("tl3", "test");
            r = Client.RPop("tl3");

            l = Client.RPushX("tl4", "test");
            r = Client.RPop("tl4");
            l = Client.RPushX("tl", "test");
            r = Client.RPop("tl");

            Destroy();
        }

        public async Task TestAsync()
        {
            Init();

            long l = await Client.LLenAsync("tl");
            l = await Client.LLenAsync("tl1");

            string r = await Client.LIndexAsync("tl", 0);
            r = await Client.LIndexAsync("tl", 101);

            var item = await Client.BLPopAsync(10, "tl");
            //item = await Client.BLPopAsync(10, "tl1");

            item = await Client.BRPopAsync(10, "tl");
            //item = await Client.BRPopAsync(10, "tl1");

            r = await Client.BRPopLPushAsync("tl", "sl", 10);
            r = await Client.BRPopLPushAsync("tl1", "sl1", 10);

            l = await Client.LPushAsync("tl2", "test");
            r = await Client.LPopAsync("tl2");

            l = await Client.LPushXAsync("tl2", "test");
            l = await Client.LPushXAsync("tl", "test");
            r = await Client.LPopAsync("tl");

            l = await Client.LRemAsync("tl", 0, "1");

            l = await Client.RPushAsync("tl3", "test");
            r = await Client.RPopAsync("tl3");

            l = await Client.RPushXAsync("tl4", "test");
            r = await Client.RPopAsync("tl4");
            l = await Client.RPushXAsync("tl", "test");
            r = await Client.RPopAsync("tl");

            Destroy();
        }

        public void Init()
        {
            for(int i = 0; i < 100; i++)
            {
                Client.LPush("tl", i.ToString());
            }
        }

        public void Destroy()
        {
            for(int i = 0; i < 100; i++)
            {
                Client.LPop("tl");
            }
        }
    }
}
