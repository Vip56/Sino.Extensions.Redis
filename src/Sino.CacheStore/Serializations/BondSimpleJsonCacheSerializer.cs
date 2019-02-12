using Bond.Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sino.CacheStore.Serializations
{
    public class BondSimpleJsonCacheSerializer : BondSerializer
    {
        public override T Deserialize<T>(byte[] data)
        {
            var value = Encoding.UTF8.GetString(data, 0, data.Length);
            using (var reader = new StringReader(value))
            {
                var jsonReader = new SimpleJsonReader(reader);
                return DeserializeInternal<SimpleJsonReader, T>(jsonReader);
            }
        }

        public override byte[] Serialize<T>(T value)
        {
            using (var writer = new StringWriter())
            {
                var jsonWriter = new SimpleJsonWriter(writer);
                SerializeInternal<SimpleJsonWriter, T>(value, jsonWriter);
                var sb = writer.GetStringBuilder();
                var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                return bytes;
            }
        }
    }
}
