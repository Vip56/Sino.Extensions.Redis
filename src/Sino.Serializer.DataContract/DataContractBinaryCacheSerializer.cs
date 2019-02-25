using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Serializer.DataContract
{
    /// <summary>
    /// Xml字节序列化
    /// </summary>
    public class DataContractBinaryCacheSerializer : ConvertProvider
    {
        public DataContractBinaryCacheSerializer SerializerSettings { get; private set; }

        public DataContractBinaryCacheSerializer(Encoding encoding, DataContractBinaryCacheSerializer serializerSettings)
            : base(encoding)
        {
            SerializerSettings = serializerSettings;
        }

        private XmlObjectSerializer GetSerializer(Type target)
        {
            if (SerializerSettings == null)
            {
                return new DataContractSerializer(target);
            }
            else
            {
                return new DataContractSerializer(target);
            }
        }

        public override T Deserialize<T>(string obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var serializer = GetSerializer(typeof(T));

        }

        public override Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override T DeserializeByte<T>(byte[] obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override string Serialize<T>(T obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override Task<string> SerializeAsync<T>(T obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override byte[] SerializeByte<T>(T obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }
    }
}
