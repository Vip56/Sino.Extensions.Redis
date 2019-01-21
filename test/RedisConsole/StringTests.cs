using Sino.Extensions.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisConsole
{
    public class StringTests
    {
        public IRedisCache Client { get; set; }

        public StringTests(IRedisCache client)
        {
            Client = client;
        }

        public void Test()
        {
            long l = Client.Append("test", "1");
            l = Client.Append("test", "2");
            Client.Remove("test");

            bool r = Client.SetBit("bit", 0, true);
            r = Client.GetBit("bit", 0);
            l = Client.BitCount("bit");
            r = Client.SetBit("bit", 1, true);
            r = Client.GetBit("bit", 1);
            l = Client.BitCount("bit");
            l = Client.BitCount("bit", 0, 0);
            Client.Remove("bit");

            Client.Set("num", "1");
            l = Client.Decr("num");
            l = Client.Incr("num");
            l = Client.IncrBy("num", 5);
            l = Client.DecrBy("num", 4);
            string s = Client.Get("num");
            Client.Remove("num");

            Client.Set("range", "aeges1232");
            s = Client.GetRange("range", 2, 4);
            l = Client.StrLen("range");
            Client.Remove("range");

            r = Client.SetNx("nx", "12321");
            r = Client.SetNx("nx", "566");
            Client.Remove("nx");
            r = Client.SetNx("nx", "566");
            Client.Remove("nx");

            Client.Set("time", "1", 5);
            Client.Set("time2", "2", 10);
            Client.Set("time3", "3", 20);
        }

        public async Task TestAsync()
        {
            long l = await Client.AppendAsync("test", "1");
            l = await Client.AppendAsync("test", "2");
            await Client.RemoveAsync("test");

            bool r = await Client.SetBitAsync("bit", 0, true);
            r = await Client.GetBitAsync("bit", 0);
            l = await Client.BitCountAsync("bit");
            r = await Client.SetBitAsync("bit", 1, true);
            r = await Client.GetBitAsync("bit", 1);
            l = await Client.BitCountAsync("bit");
            l = await Client.BitCountAsync("bit", 0, 0);
            await Client.RemoveAsync("bit");

            await Client.SetAsync("num", "1");
            l = await Client.DecrAsync("num");
            l = await Client.IncrAsync("num");
            l = await Client.IncrByAsync("num", 5);
            l = await Client.DecrByAsync("num", 4);
            string s = await Client.GetAsync("num");
            await Client.RemoveAsync("num");

            await Client.SetAsync("range", "aeges1232");
            s = await Client.GetRangeAsync("range", 2, 4);
            l = await Client.StrLenAsync("range");
            await Client.RemoveAsync("range");

            r = await Client.SetNxAsync("nx", "12321");
            r = await Client.SetNxAsync("nx", "566");
            await Client.RemoveAsync("nx");
            r = await Client.SetNxAsync("nx", "566");
            await Client.RemoveAsync("nx");

            await Client.SetAsync("time", "1", 5);
            await Client.SetAsync("time2", "2", 10);
            await Client.SetAsync("time3", "3", 20);
        }
    }
}
