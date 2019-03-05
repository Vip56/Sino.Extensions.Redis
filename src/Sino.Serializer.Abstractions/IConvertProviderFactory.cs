using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.Abstractions
{
    /// <summary>
    /// 提供器工厂接口
    /// </summary>
    public interface IConvertProviderFactory
    {
        /// <summary>
        /// 获取默认的序列化提供器
        /// </summary>
        /// <returns>序列化提供器对象</returns>
        IConvertProvider GetDefaultConvertProvider();

        /// <summary>
        /// 根据名称获取对应序列化提供器
        /// </summary>
        /// <param name="name">序列化提供器名称</param>
        /// <returns>序列化提供器对象</returns>
        IConvertProvider GetConvertProvider(string name);

        /// <summary>
        /// 根据组名获取对应序列化提供器组
        /// </summary>
        /// <param name="groupName">组别名称</param>
        /// <returns>序列化提供器组</returns>
        IList<IConvertProvider> GetConvertProviders(string groupName);

        /// <summary>
        /// 获取所有序列化提供器名称
        /// </summary>
        /// <returns>名称数组</returns>
        IList<string> GetConvertProviderNames();

        /// <summary>
        /// 获取所有序列化提供器组名
        /// </summary>
        /// <returns>组名数组</returns>
        IList<string> GetConvertProviderGroupNames();
    }
}
