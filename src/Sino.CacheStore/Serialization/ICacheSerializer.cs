using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Serializations
{
    /// <summary>
    /// 序列化基础接口
    /// </summary>
    public interface ICacheSerializer
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">需要被序列化对象的类型</typeparam>
        /// <param name="value">需要序列化的值</param>
        /// <returns>序列化后的值</returns>
        byte[] Serialize<T>(T value) where T: class;

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T">需要序列化成的对象</typeparam>
        /// <param name="data">需要序列化的数据</param>
        /// <returns>序列化后的对象</returns>
        T Deserialize<T>(byte[] data) where T: class;
    }
}
