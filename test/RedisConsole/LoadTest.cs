using Sino.Extensions.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace RedisConsole
{
    public class LoadTest
    {
        public IRedisCache Client { get; set; }

        public LoadTest(IRedisCache client)
        {
            Client = client;
        }

        public void Test()
        {
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < 5000; i++)
            {
                Client.Set("key" + i.ToString(), "val" + i.ToString(), 100);
            }
            watch.Stop();
            Console.WriteLine($"Set 5000 count, use {watch.ElapsedMilliseconds} ms");
            watch.Restart();
            for (int i = 0; i< 5000; i++)
            {
                Client.Get("key" + i.ToString());
            }
            watch.Stop();
            Console.WriteLine($"Get 5000 count, use {watch.ElapsedMilliseconds} ms");
        }

        public void TestParallel()
        {
            Stopwatch watch = Stopwatch.StartNew();
            Enumerable.Range(0, 100000).AsParallel().
                ForAll(x => Client.Set("pkey" + x.ToString(), "pval" + x.ToString(), 100));
            watch.Stop();
            Console.WriteLine($"Set 10000 count parallel, use {watch.ElapsedMilliseconds} ms");
            watch.Restart();
            Enumerable.Range(0, 100000).AsParallel()
                .ForAll(x => Client.Get("pkey" + x.ToString()));
            watch.Stop();
            Console.WriteLine($"Get 10000 count parallel, use {watch.ElapsedMilliseconds} ms");
        }

        public void TestParallelAsync()
        {
            Stopwatch watch = Stopwatch.StartNew();
            Enumerable.Range(0, 10000).AsParallel()
                .ForAll(async x => await Client.SetAsync("pakey" + x.ToString(), "paval" + x.ToString(), 100));
            watch.Stop();
            Console.WriteLine($"SetAsync 10000 count parallel, use {watch.ElapsedMilliseconds} ms");
            watch.Restart();
            Enumerable.Range(0, 10000).AsParallel()
                .ForAll(async x => await Client.GetAsync("pakey" + x.ToString()));
            watch.Stop();
            Console.WriteLine($"GetAsync 10000 count parallel, use {watch.ElapsedMilliseconds} ms");
        }

        public async Task TestAsync()
        {
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i< 5000; i++)
            {
                await Client.SetAsync("akey" + i, "val" + i, 100);
            }
            watch.Stop();
            Console.WriteLine($"SetAsync 5000 count, use {watch.ElapsedMilliseconds} ms");
            watch.Restart();
            for (int i = 0; i < 5000; i++)
            {
                await Client.GetAsync("akey" + i);
            }
            watch.Stop();
            Console.WriteLine($"GetAsync 5000 count, use {watch.ElapsedMilliseconds} ms");
        }
    }
}
