using Microsoft.Extensions.DependencyInjection;
using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SinoSerializerCollectionExtensions
    {
        /// <summary>
        /// 添加序列化提供器
        /// </summary>
        /// <param name="setupAction">配置</param>
        public static IServiceCollection AddSinoSerializer(this IServiceCollection services, Action<SerializerSettingsBuilder> setupAction)
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
            services.Add(ServiceDescriptor.Singleton<IConvertProviderFactory, ConvertProviderFactory>());
            services.Add(ServiceDescriptor.Singleton<IConvertProvider>(s =>
            {
                var factory = s.GetService<IConvertProviderFactory>();
                return factory.GetDefaultConvertProvider();
            }));

            return services;
        }
    }
}
