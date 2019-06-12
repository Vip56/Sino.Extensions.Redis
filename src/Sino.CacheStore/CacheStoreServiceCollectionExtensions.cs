using Microsoft.Extensions.Configuration;
using Sino.CacheStore.Configuration;
using System;
using System.Linq;
using Sino.CacheStore;
using Sino.CacheStore.Handler;
using Sino.CacheStore.Internal;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CacheStoreServiceCollectionExtensions
    {
        /// <summary>
        /// 配置根节点名
        /// </summary>
        public const string CACHE_STORE_SECTION = "cachestore";

        /// <summary>
        /// 注册缓存存储
        /// </summary>
        /// <param name="fromConfiguration">配置对象<paramref name="configuration"></param>
        /// <exception cref="InvalidOperationException">如果配置不存在或多于一个触发异常</exception>
        public static IServiceCollection AddCacheStore(this IServiceCollection services, IConfiguration fromConfiguration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (fromConfiguration == null)
                throw new ArgumentNullException(nameof(fromConfiguration));

            var section = fromConfiguration.GetSection(CACHE_STORE_SECTION);
            if (section.GetChildren().Count() == 0)
            {
                throw new InvalidOperationException($"No {CACHE_STORE_SECTION} section found in the configuration provided.");
            }

            if (section.GetChildren().Count() > 1)
            {
                throw new InvalidOperationException($"The {CACHE_STORE_SECTION} section has more than one configuration defined.");
            }

            services.Configure<CacheStoreOptions>(fromConfiguration.GetSection(CACHE_STORE_SECTION));
            services.Add(ServiceDescriptor.Singleton<ICacheStore, CacheStoreClient>());
            services.Add(ServiceDescriptor.Singleton<ICacheStoreHandler, RedisCacheStoreHandler>());
            services.Add(ServiceDescriptor.Singleton<CommandFactory, RedisCommandFactory>());

            return services;
        }

        /// <summary>
        /// 注册缓存存储
        /// </summary>
        /// <param name="configureOptions">设置配置项</param>
        public static IServiceCollection AddCacheStore(this IServiceCollection services, Action<CacheStoreOptions> configureOptions)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            services.Configure(configureOptions);
            services.Add(ServiceDescriptor.Singleton<ICacheStore, CacheStoreClient>());
            services.Add(ServiceDescriptor.Singleton<ICacheStoreHandler, RedisCacheStoreHandler>());
            services.Add(ServiceDescriptor.Singleton<CommandFactory, RedisCommandFactory>());

            return services;
        }
    }
}
