using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Sino.CacheStore;
using Sino.CacheStore.Configuration;
using Sino.Serializer.Json;
using System;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));

            // 注入缓存存储
            services.AddCacheStore(x => new CacheStoreOptions());

            // 注入Json序列化方法
            services.AddSinoSerializer(x => x.AddJsonSerializer()
                .SetDefaultConvertProvider(JsonConvertProvider.PROVIDER_NAME));

            var provider = services.BuildServiceProvider();

            var cacheStore = provider.GetService<ICacheStore>();

            var key = new KeyTest(cacheStore);
            key.ExistsTest().Wait();
        }
    }
}
