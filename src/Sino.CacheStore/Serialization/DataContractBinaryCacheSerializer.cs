using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Sino.CacheStore.Serialization
{
    public class DataContractBinaryCacheSerializer : CacheSerializer
    {
        public DataContractSerializerSettings SerializerSettings { get; private set; }

        public DataContractBinaryCacheSerializer()
        {

        }

        public override T Deserialize<T>(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var serializer = GetSerializer(typeof(T));
            using (var stream = new MemoryStream(data))
            {
                var binaryReader = XmlDictionaryReader.CreateBinaryReader(stream, new XmlDictionaryReaderQuotas());
                return serializer.ReadObject(binaryReader) as T;
            }
        }

        public override byte[] Serialize<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var serializer = GetSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                var binaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream);
                serializer.WriteObject(binaryWriter, value);
                binaryWriter.Flush();
                return stream.ToArray();
            }
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
    }
}
