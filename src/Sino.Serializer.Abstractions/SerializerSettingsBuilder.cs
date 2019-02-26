using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using Sino.Serializer.Abstractions.Exceptions;

namespace Sino.Serializer.Abstractions
{
    /// <summary>
    /// 配置
    /// </summary>
    public class SerializerSettingsBuilder
    {
        private ConcurrentDictionary<string, Func<IConvertProvider>> _convertProviders;

        public Encoding GlobalEncoding = Encoding.UTF8;

        public string DefaultConvertProviderName { get; private set; }

        public SerializerSettingsBuilder()
        {
            _convertProviders = new ConcurrentDictionary<string, Func<IConvertProvider>>();
        }

        /// <summary>
        /// 添加序列化提供器
        /// </summary>
        /// <param name="name">提供器名称</param>
        /// <param name="convertProvider">提供器对象</param>
        public void AddProvider(string name, Func<IConvertProvider> convertProvider)
        {
            if (_convertProviders.ContainsKey(name))
                throw new ConvertProviderExistedException($"The {name} ConvertProvider is exited");

            _convertProviders.AddOrUpdate(name, convertProvider, (x, y) =>
            {
                return convertProvider;
            });
        }

        /// <summary>
        /// 移除序列化提供器
        /// </summary>
        /// <param name="name"></param>
        public void RemoveProvider(string name)
        {
            if (!_convertProviders.ContainsKey(name))
                return;

            Func<IConvertProvider> provider = null;
            _convertProviders.TryRemove(name, out provider);
        }

        /// <summary>
        /// 设置默认的序列化提供器
        /// </summary>
        /// <param name="name"></param>
        public void SetDefaultConvertProvider(string name)
        {
            if (!_convertProviders.ContainsKey(name))
                throw new KeyNotFoundException($"The {name} ConvertProvider is not existed");
            DefaultConvertProviderName = name;
        }
    }
}
