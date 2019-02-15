using Bond.IO.Safe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CompactWriter = Bond.Protocols.CompactBinaryWriter<Bond.IO.Safe.OutputBuffer>;
using CompactReader = Bond.Protocols.CompactBinaryReader<Bond.IO.Safe.InputBuffer>;

namespace Sino.Serializer.Bond
{
    /// <summary>
    /// 紧凑二进制序列化
    /// </summary>
    public class BondCompactBinaryConvertProvider : BondConvertProvider
    {
        public BondCompactBinaryConvertProvider(Encoding encoding)
            : base(encoding) { }

        public override T Deserialize<T>(string obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var value = encoding.GetBytes(obj);
            var input = new InputBuffer(value);
            var reader = new CompactReader(input);
            return DeserializeInternal<CompactReader, T>(reader);
        }

        public override Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;
            var value = encoding.GetBytes(obj);
            var input = new InputBuffer(value);
            var reader = new CompactReader(input);
            return Task.FromResult(DeserializeInternal<CompactReader, T>(reader));
        }

        public override T DeserializeByte<T>(byte[] obj, Encoding encoding = null)
        {
            var input = new InputBuffer(obj);
            var reader = new CompactReader(input);
            return DeserializeInternal<CompactReader, T>(reader);
        }

        public override Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null)
        {
            var input = new InputBuffer(obj);
            var reader = new CompactReader(input);
            return Task.FromResult(DeserializeInternal<CompactReader, T>(reader));
        }

        public override string Serialize<T>(T obj, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var output = new OutputBuffer();
            var writer = new CompactWriter(output);
            SerializeInternal<CompactWriter, T>(obj, writer);
            return encoding.GetString(output.Data.Array);
        }

        public override Task<string> SerializeAsync<T>(T obj, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var output = new OutputBuffer();
            var writer = new CompactWriter(output);
            SerializeInternal<CompactWriter, T>(obj, writer);
            return Task.FromResult(encoding.GetString(output.Data.Array));
        }

        public override byte[] SerializeByte<T>(T obj)
        {
            var output = new OutputBuffer();
            var writer = new CompactWriter(output);
            SerializeInternal<CompactWriter, T>(obj, writer);
            return output.Data.Array;
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj)
        {
            var output = new OutputBuffer();
            var writer = new CompactWriter(output);
            SerializeInternal<CompactWriter, T>(obj, writer);
            return Task.FromResult(output.Data.Array);
        }
    }
}
