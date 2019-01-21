using Sino.Extensions.Redis;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RedisCacheServiceCollectionExtensions
    {
        /// <summary>
        /// 增加Redis扩展
        /// </summary>
        /// <param name="setupAction">配置</param>
        public static IServiceCollection AddRedisCache(this IServiceCollection services, Action<RedisCacheOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.Add(ServiceDescriptor.Singleton<IRedisCache, RedisCache>());

            return services;
        }
    }
}
