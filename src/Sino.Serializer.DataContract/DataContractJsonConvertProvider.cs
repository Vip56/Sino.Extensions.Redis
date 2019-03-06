using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Serializer.DataContract
{
    public class DataContractJsonConvertProvider : ConvertProvider
    {
        public const string PROVIDER_NAME = "json";

        public DataContractJsonSerializerSettings SerializerSettings { get; private set; }

        public DataContractJsonConvertProvider(Encoding encoding, DataContractJsonSerializerSettings serializerSettings)
            : base(encoding)
        {
            SerializerSettings = serializerSettings;
        }

        private XmlObjectSerializer GetSerializer(Type target)
        {
            if (SerializerSettings == null)
            {
                return new DataContractJsonSerializer(target);
            }
            else
            {
                return new DataContractJsonSerializer(target, SerializerSettings);
            }
        }

        public override T Deserialize<T>(string obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var data = encoding.GetBytes(obj);
            return DeserializeByte<T>(data, encoding);
        }

        public override Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null)
        {
            return Task.FromResult(Deserialize<T>(obj, encoding));
        }

        public override T DeserializeByte<T>(byte[] obj, Encoding encoding = null)
        {
            var serializer = GetSerializer(typeof(T));
            using (var ms = new MemoryStream(obj))
            {
                return serializer.ReadObject(ms) as T;
            }
        }

        public override Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null)
        {
            return Task.FromResult(DeserializeByte<T>(obj, encoding));
        }

        public override string Serialize<T>(T obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var data = SerializeByte<T>(obj, encoding);
            return encoding.GetString(data);
        }

        public override Task<string> SerializeAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(Serialize<T>(obj, encoding));
        }

        public override byte[] SerializeByte<T>(T obj, Encoding encoding = null)
        {
            var serializer = GetSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return ms.ToArray();
            }
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(SerializeByte<T>(obj, encoding));
        }
    }
}
