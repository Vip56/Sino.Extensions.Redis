using Bond.IO.Safe;
using System;
using System.Collections.Generic;
using System.Text;
using CompactWriter = Bond.Protocols.CompactBinaryWriter<Bond.IO.Safe.OutputBuffer>;
using CompactReader = Bond.Protocols.CompactBinaryReader<Bond.IO.Safe.InputBuffer>;

namespace Sino.CacheStore.Serializations
{
    /// <summary>
    /// Bond序列化
    /// </summary>
    public class BondCompactBinaryCacheSerializer : BondSerializer
    {
        public override T Deserialize<T>(byte[] data)
        {
            var input = new InputBuffer(data);
            var reader = new CompactReader(input);
            return DeserializeInternal<CompactReader, T>(reader);
        }

        public override byte[] Serialize<T>(T value)
        {
            var output = new OutputBuffer();
            var writer = new CompactWriter(output);
            SerializeInternal<CompactWriter, T>(value, writer);
            return output.Data.Array;
        }
    }
}
