using Bond;
using Bond.IO.Safe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Serializations
{
    /// <summary>
    /// Bond序列化基类
    /// </summary>
    public abstract class BondSerializer : CacheSerializer
    {
        /// <summary>
        /// 内部标准序列化
        /// </summary>
        /// <typeparam name="TWriter">输出流类型</typeparam>
        /// <param name="val">需要序列化的值</param>
        /// <param name="writer">输出流对象</param>
        public void SerializeInternal<TWriter, T>(T val, TWriter writer) where T: class
        {
            var serializer = new Serializer<TWriter>(typeof(T));
            serializer.Serialize(val, writer);
        }

        /// <summary>
        /// 内部标准反序列化
        /// </summary>
        /// <typeparam name="TReader">输入流类型</typeparam>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="reader">输入流对象</param>
        /// <returns>反序列化后的对象</returns>
        public T DeserializeInternal<TReader, T>(TReader reader) where T: class
        {
            var deserializer = new Deserializer<TReader>(typeof(T));
            return deserializer.Deserialize(reader) as T;
        }
    }
}
