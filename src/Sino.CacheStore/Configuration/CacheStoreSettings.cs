using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Configuration
{
    public class CacheStoreSettings
    {
        /// <summary>
        /// Json与GzJson序列化配置，默认为Null
        /// </summary>
        private JsonSerializerSettings _jsonSerializationSettings;

        /// <summary>
        /// Json与GzJson反序列化配置，默认为Null
        /// </summary>
        private JsonSerializerSettings _jsonDeserializationSettings;

        /// <summary>
        /// 使用Json序列化
        /// </summary>
        public bool UseJsonSerialization { get; private set; }

        public CacheStoreSettings()
        {

        }

        /// <summary>
        /// 序列化格式，默认为Json
        /// </summary>
        public SerializerType DefaultSerializationType { get; private set; } = SerializerType.Json;

        /// <summary>
        /// 支持Json序列化
        /// </summary>
        /// <param name="isDefault">是否为默认序列化格式</param>
        public CacheStoreSettings AddJson(bool isDefault = false)
        {
            UseJsonSerialization = true;
            if (isDefault)
            {
                DefaultSerializationType = SerializerType.Json;
            }
            return this;
        }

        /// <summary>
        /// 支持Json序列化
        /// </summary>
        /// <param name="serializationSettings">序列化设置</param>
        /// <param name="deserializationSettings">反序列化设置</param>
        /// <param name="isDefault">是否为默认序列化格式</param>
        public CacheStoreSettings AddJson(JsonSerializerSettings serializationSettings, JsonSerializerSettings deserializationSettings, bool isDefault = false)
        {
            _jsonSerializationSettings = serializationSettings ?? throw new ArgumentNullException(nameof(serializationSettings));
            _jsonDeserializationSettings = deserializationSettings ?? throw new ArgumentNullException(nameof(deserializationSettings));
            return AddJson(isDefault);
        }
    }

    /// <summary>
    /// 序列化类型
    /// </summary>
    public enum SerializerType
    {
        BondCompactBinary,
        BondFastBinary,
        BondSimpleJson,
        DataContract,
        Json,
        GzJson,
        ProtoBuf
    }
}
