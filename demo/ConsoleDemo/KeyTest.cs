using Sino.CacheStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    public class KeyTest
    {
        protected ICacheStore CacheStore { get; set; }

        public KeyTest(ICacheStore cacheStore)
        {
            CacheStore = cacheStore;
        }

        public async Task ExistsTest()
        {
            var r = CacheStore.Exists("test2");
            r = await CacheStore.ExistsAsync("test2");

            var s = CacheStore.SetBytes("test2", Encoding.UTF8.GetBytes("value1"));
            var v = Encoding.UTF8.GetString(CacheStore.GetBytes("test2"));

            r = CacheStore.Exists("test2");

            Parallel.For(1, 10000, x =>
            {
                CacheStore.SetBytes($"test{x}", Encoding.UTF8.GetBytes($"value{x}"));
                var str = Encoding.UTF8.GetString(CacheStore.GetBytes($"test{x}"));
                if (str != $"value{x}")
                {
                    Console.WriteLine($"Failed {x}");
                }
                CacheStore.Remove($"test{x}");
            });
        }
    }
}
