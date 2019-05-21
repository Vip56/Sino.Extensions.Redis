using Newtonsoft.Json;
using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Serializer.Json
{
    public class JsonConvertProvider : ConvertProvider
    {
        public const string PROVIDER_NAME = "json_normal";

        private readonly JsonSerializerSettings _serializerSettings;

        public JsonConvertProvider(Encoding encoding, JsonSerializerSettings settings)
            : base(encoding)
        {
            _serializerSettings = settings;
        }

        public override T Deserialize<T>(string obj, Encoding encoding = null)
        {
            if (_serializerSettings == null)
            {
                return JsonConvert.DeserializeObject<T>(obj);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(obj, _serializerSettings);
            }
        }

        public override Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null)
        {
            return Task.FromResult(Deserialize<T>(obj, encoding));
        }

        public override T DeserializeByte<T>(byte[] obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var data = encoding.GetString(obj);
            return Deserialize<T>(data);
        }

        public override Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null)
        {
            return Task.FromResult(DeserializeByte<T>(obj, encoding));
        }

        public override string Serialize<T>(T obj, Encoding encoding = null)
        {
            if (_serializerSettings == null)
            {
                return JsonConvert.SerializeObject(obj);
            }
            else
            {
                return JsonConvert.SerializeObject(obj, _serializerSettings);
            }
        }

        public override Task<string> SerializeAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(Serialize<T>(obj, encoding));
        }

        public override byte[] SerializeByte<T>(T obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var data = Serialize<T>(obj, encoding);
            return encoding.GetBytes(data);
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(SerializeByte<T>(obj, encoding));
        }
    }
}
