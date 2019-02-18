using Bond.Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Serializer.Bond
{
    /// <summary>
    /// Json格式化
    /// </summary>
    public class BondSimpleJsonConvertProvider : BondConvertProvider
    {
        public BondSimpleJsonConvertProvider(Encoding encoding)
            : base(encoding) { }

        public override T Deserialize<T>(string obj, Encoding encoding = null)
        {
            using (var reader = new StringReader(obj))
            {
                var jsonReader = new SimpleJsonReader(reader);
                return DeserializeInternal<SimpleJsonReader, T>(jsonReader);
            }
        }

        public override Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null)
        {
            return Task.FromResult(Deserialize<T>(obj, encoding));
        }

        public override T DeserializeByte<T>(byte[] obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var value = encoding.GetString(obj, 0, obj.Length);
            return Deserialize<T>(value, encoding);
        }

        public override Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null)
        {
            return Task.FromResult(DeserializeByte<T>(obj, encoding));
        }

        public override string Serialize<T>(T obj, Encoding encoding = null)
        {
            using (var writer = new StringWriter())
            {
                var jsonWriter = new SimpleJsonWriter(writer);
                SerializeInternal<SimpleJsonWriter, T>(obj, jsonWriter);
                var sb = writer.GetStringBuilder();
                return sb.ToString();
            }
        }

        public override Task<string> SerializeAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(Serialize<T>(obj, encoding));
        }

        public override byte[] SerializeByte<T>(T obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            using (var writer = new StringWriter())
            {
                var jsonWriter = new SimpleJsonWriter(writer);
                SerializeInternal<SimpleJsonWriter, T>(obj, jsonWriter);
                var sb = writer.GetStringBuilder();
                return encoding.GetBytes(sb.ToString());
            }
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(SerializeByte<T>(obj, encoding));
        }
    }
}
