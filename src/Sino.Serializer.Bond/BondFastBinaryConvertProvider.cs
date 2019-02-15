using Bond.IO.Safe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FastWriter = Bond.Protocols.FastBinaryWriter<Bond.IO.Safe.OutputBuffer>;
using FastReader = Bond.Protocols.FastBinaryReader<Bond.IO.Safe.InputBuffer>;

namespace Sino.Serializer.Bond
{
    /// <summary>
    /// 快速二进制
    /// </summary>
    public class BondFastBinaryConvertProvider : BondConvertProvider
    {
        public BondFastBinaryConvertProvider(Encoding encoding)
            : base(encoding) { }

        public override T Deserialize<T>(string obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var value = encoding.GetBytes(obj);
            var input = new InputBuffer(value);
            var reader = new FastReader(input);
            return DeserializeInternal<FastReader, T>(reader);
        }

        public override Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null)
        {
            return Task.FromResult(Deserialize<T>(obj, encoding));
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

        public override byte[] SerializeByte<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
