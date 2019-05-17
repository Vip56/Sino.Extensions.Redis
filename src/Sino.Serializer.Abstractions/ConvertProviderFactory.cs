using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.Abstractions
{
    /// <summary>
    /// 序列化提供器工厂实现类
    /// </summary>
    public class ConvertProviderFactory : IConvertProviderFactory
    {
        protected ConcurrentDictionary<string, Lazy<IConvertProvider>> ConvertProviders { get; set; } = new ConcurrentDictionary<string, Lazy<IConvertProvider>>();

        protected IConvertProvider DefaultConvertPrivoder { get; set; }

        protected ILogger<ConvertProviderFactory> Logger { get; set; }

        public ConvertProviderFactory(IOptions<SerializerSettingsBuilder> optionsAccessor, ILogger<ConvertProviderFactory> logger)
            : this(optionsAccessor?.Value, logger) { }

        public ConvertProviderFactory(SerializerSettingsBuilder options, ILogger<ConvertProviderFactory> logger)
        {
            Logger = logger;
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.CopyTo(ConvertProviders);
            SetDefaultConvertProvider(options.DefaultConvertProviderName);

            Logger.LogDebug("Factory init.");
        }

        /// <summary>
        /// 设定默认序列化提供器
        /// </summary>
        /// <param name="name">默认序列化提供器名称</param>
        protected virtual void SetDefaultConvertProvider(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            DefaultConvertPrivoder = GetConvertProvider(name);
        }

        public IConvertProvider GetDefaultConvertProvider()
        {
            return DefaultConvertPrivoder;
        }

        public IConvertProvider GetConvertProvider(string name)
        {
            Logger.LogDebug("Get {Name} ConvertProvider.", name);

            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (ConvertProviders.TryGetValue(name, out Lazy<IConvertProvider> provider))
            {
                return provider.Value;
            }
            throw new KeyNotFoundException($"The {name} ConvertProvider is not existed.");
        }

        public IEnumerable<string> GetConvertProviderGroupNames()
        {
            var keys = ConvertProviders.Keys;
            foreach(var key in keys)
            {
                var splits = key.Split('_');
                if (splits.Length > 1)
                    yield return splits[0];
            }
        }

        public IEnumerable<string> GetConvertProviderNames()
        {
            return ConvertProviders.Keys;
        }

        public IEnumerable<IConvertProvider> GetConvertProviders(string groupName)
        {
            foreach(var kv in ConvertProviders)
            {
                if (kv.Key.StartsWith(groupName))
                    yield return kv.Value.Value;
            }
        }
    }
}
