using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Serialization
{
    /// <summary>
    /// Json序列化
    /// </summary>
    public class JsonCacheSerializer : CacheSerializer
    {
        private readonly JsonSerializerSettings _deserializerSettings;
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonCacheSerializer()
            : this(new JsonSerializerSettings(), new JsonSerializerSettings())
        {

        }

        public JsonCacheSerializer(JsonSerializerSettings serializerSettings, JsonSerializerSettings deserializationSettings)
        {
            _deserializerSettings = deserializationSettings;
            _serializerSettings = serializerSettings;
        }

        public override T Deserialize<T>(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            string jsonStr = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(jsonStr, _deserializerSettings);
        }

        public override byte[] Serialize<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            string jsonStr = JsonConvert.SerializeObject(value, _serializerSettings);
            return Encoding.UTF8.GetBytes(jsonStr);
        }
    }
}
