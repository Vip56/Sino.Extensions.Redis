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
        public const string PROVIDER_NAME = "fb";

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
            var input = new InputBuffer(obj);
            var reader = new FastReader(input);
            return DeserializeInternal<FastReader, T>(reader);
        }

        public override Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null)
        {
            return Task.FromResult(DeserializeByte<T>(obj, encoding));
        }

        public override string Serialize<T>(T obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            return encoding.GetString(SerializeByte<T>(obj));
        }

        public override Task<string> SerializeAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(Serialize<T>(obj, encoding));
        }

        public override byte[] SerializeByte<T>(T obj, Encoding encoding = null)
        {
            var output = new OutputBuffer();
            var writer = new FastWriter(output);
            SerializeInternal<FastWriter, T>(obj, writer);
            return output.Data.Array;
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(SerializeByte<T>(obj));
        }
    }
}
