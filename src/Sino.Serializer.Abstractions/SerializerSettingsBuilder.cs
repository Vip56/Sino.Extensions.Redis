using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using Sino.Serializer.Abstractions.Exceptions;
using Microsoft.Extensions.Options;

namespace Sino.Serializer.Abstractions
{
    /// <summary>
    /// 配置
    /// </summary>
    public class SerializerSettingsBuilder : IOptions<SerializerSettingsBuilder>
    {
        private ConcurrentDictionary<string, Func<IConvertProvider>> _convertProviders;

        public Encoding GlobalEncoding = Encoding.UTF8;

        public string DefaultConvertProviderName { get; private set; }

        public SerializerSettingsBuilder Value => this;

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
        public bool RemoveProvider(string name)
        {
            if (!_convertProviders.ContainsKey(name))
                return false;
            if (name == DefaultConvertProviderName)
                return false;

            Func<IConvertProvider> provider = null;
            _convertProviders.TryRemove(name, out provider);
            return true;
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

        /// <summary>
        /// 将对象复制到新目标
        /// </summary>
        /// <param name="destination">复制到的对象</param>
        public void CopyTo(IDictionary<string, Func<IConvertProvider>> destination)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            if (_convertProviders.Count <= 0)
                return;

            foreach(var value in _convertProviders)
            {
                destination.Add(value.Key, value.Value);
            }
        }
    }
}
