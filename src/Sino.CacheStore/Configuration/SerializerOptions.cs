using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Configuration
{
    /// <summary>
    /// 序列化设置
    /// </summary>
    public class SerializerOptions
    {
        /// <summary>
        /// 序列化格式，默认为Json
        /// </summary>
        public SerializerType DefaultType { get; set; } = SerializerType.Json;

        /// <summary>
        /// Json与GzJson序列化配置，默认为Null
        /// </summary>
        public JsonSerializerSettings JsonSerializationSettings { get; set; }

        /// <summary>
        /// Json与GzJson反序列化配置，默认为Null
        /// </summary>
        public JsonSerializerSettings JsonDeserializationSettings { get; set; }


    }

    /// <summary>
    /// 序列化类型
    /// </summary>
    public enum SerializerType
    {
        Bond,
        DataContract,
        Json,
        GzJson,
        ProtoBuf
    }
}
