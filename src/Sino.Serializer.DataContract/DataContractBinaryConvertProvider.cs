using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sino.Serializer.DataContract
{
    /// <summary>
    /// Xml字节序列化
    /// </summary>
    public class DataContractBinaryConvertProvider : ConvertProvider
    {
        public const string PROVIDER_NAME = "datacontract_binary";

        public DataContractSerializerSettings SerializerSettings { get; private set; }

        public DataContractBinaryConvertProvider(Encoding encoding, DataContractSerializerSettings serializerSettings)
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
                return new DataContractSerializer(target, SerializerSettings);
            }
        }

        public override T Deserialize<T>(string obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override T DeserializeByte<T>(byte[] obj, Encoding encoding = null)
        {
            var serializer = GetSerializer(typeof(T));
            using (var stream = new MemoryStream(obj))
            {
                var binaryReader = XmlDictionaryReader.CreateBinaryReader(stream, new XmlDictionaryReaderQuotas());
                return serializer.ReadObject(binaryReader) as T;
            }
        }

        public override Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null)
        {
            return Task.FromResult(DeserializeByte<T>(obj, encoding));
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
            var serializer = GetSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                var binaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream);
                serializer.WriteObject(binaryWriter, obj);
                binaryWriter.Flush();
                stream.Flush();
                return stream.ToArray();
            }
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(SerializeByte<T>(obj, encoding));
        }
    }
}
