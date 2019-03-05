using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.Abstractions
{
    /// <summary>
    /// 序列化提供器工厂实现类
    /// </summary>
    public class ConvertProviderFactory : IConvertProviderFactory
    {
        public ConvertProviderFactory(IOptions<SerializerSettingsBuilder> builder)
        {

        }

        public IConvertProvider GetDefaultConvertProvider()
        {
            throw new NotImplementedException();
        }

        public IConvertProvider GetConvertProvider(string name)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetConvertProviderGroupNames()
        {
            throw new NotImplementedException();
        }

        public IList<string> GetConvertProviderNames()
        {
            throw new NotImplementedException();
        }

        public IList<IConvertProvider> GetConvertProviders(string groupName)
        {
            throw new NotImplementedException();
        }
    }
}
