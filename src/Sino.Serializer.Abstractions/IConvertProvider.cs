using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Serializer.Abstractions
{
    public interface IConvertProvider
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="obj">实际对象</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>序列化后的字符串</returns>
        string Serialize<T>(T obj, Encoding encoding = null) where T : class;

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="obj">实际对象</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>序列化后的字符串</returns>
        Task<string> SerializeAsync<T>(T obj, Encoding encoding = null) where T : class;

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="obj">实际对象</param>
        /// <returns>序列化后的字节</returns>
        byte[] SerializeByte<T>(T obj) where T : class;

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="obj">实际对象</param>
        /// <returns>序列化后的字节</returns>
        Task<byte[]> SerializeByteAsync<T>(T obj) where T : class;

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">实际对象</param>
        /// <returns>反序列化后的对象</returns>
        T Deserialize<T>(string obj, Encoding encoding = null) where T : class;

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">实际对象</param>
        /// <returns>反序列化后的对象</returns>
        Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null) where T : class;

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">实际对象</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>反序列化后的对象</returns>
        T DeserializeByte<T>(byte[] obj, Encoding encoding = null) where T : class;

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">实际对象</param>
        /// <param name="encoding">编码格式，默认为UTF-8</param>
        /// <returns>反序列化后的对象</returns>
        Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null) where T : class;
    }
}
