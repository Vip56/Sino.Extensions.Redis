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
        private ConcurrentDictionary<string, Action<IConvertProvider>> _convertProviders;

        public SerializerSettingsBuilder()
        {
            _convertProviders = new ConcurrentDictionary<string, Action<IConvertProvider>>();
        }

        /// <summary>
        /// 添加序列化提供器
        /// </summary>
        /// <param name="name">提供器名称</param>
        /// <param name="convertProvider">提供器对象</param>
        public void AddProvider(string name, Action<IConvertProvider> convertProvider)
        {
            if (_convertProviders.ContainsKey(name))
                throw new ConvertProviderExistedException($"The {name} ConvertProvider is exited");

            _convertProviders.AddOrUpdate(name, convertProvider, (x, y) =>
            {
                return convertProvider;
            });
        }
    }
}
