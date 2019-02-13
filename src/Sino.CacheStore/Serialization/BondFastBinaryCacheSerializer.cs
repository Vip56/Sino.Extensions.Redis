using System;
using System.Collections.Generic;
using System.Text;
using FastWriter = Bond.Protocols.FastBinaryWriter<Bond.IO.Safe.OutputBuffer>;
using FastReader = Bond.Protocols.FastBinaryReader<Bond.IO.Safe.InputBuffer>;
using Bond.IO.Safe;

namespace Sino.CacheStore.Serialization
{
    /// <summary>
    /// Bond序列化
    /// </summary>
    public class BondFastBinaryCacheSerializer : BondSerializer
    {
        public override byte[] Serialize<T>(T value)
        {
            var output = new OutputBuffer();
            var writer = new FastWriter(output);
            SerializeInternal<FastWriter, T>(value, writer);
            return output.Data.Array;
        }

        public override T Deserialize<T>(byte[] data)
        {
            var input = new InputBuffer(data);
            var reader = new FastReader(input);
            return DeserializeInternal<FastReader, T>(reader);
        }
    }
}
