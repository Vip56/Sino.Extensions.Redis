using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Sino.CacheStore.Serialization
{
    public class DataContractGzJsonCacheSerializer : CacheSerializer
    {
        public DataContractJsonSerializerSettings SerializerSettings { get; private set; }

        public DataContractGzJsonCacheSerializer()
        {

        }

        public override T Deserialize<T>(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var serializer = GetSerializer(typeof(T));
            using (var ms = new MemoryStream(data))
            {
                using (var gs = new GZipStream(ms, CompressionMode.Decompress))
                {
                    return serializer.ReadObject(gs) as T;
                }
            }
        }

        public override byte[] Serialize<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var serializer = GetSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                using (var gs = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    serializer.WriteObject(gs, value);
                    gs.Flush();
                }
                return ms.ToArray();
            }
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
    }
}
