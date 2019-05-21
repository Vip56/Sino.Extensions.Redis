using Newtonsoft.Json;
using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Sino.Serializer.Json
{
    public class GzJsonConvertProvider : ConvertProvider
    {
        public const string PROVIDER_NAME = "json_gzjson";

        private readonly JsonSerializerSettings _serializerSettings;

        public GzJsonConvertProvider(Encoding encoding, JsonSerializerSettings settings)
            :base(encoding)
        {
            _serializerSettings = settings;
        }

        /// <summary>
        /// 压缩内容
        /// </summary>
        private byte[] Compression(byte[] data)
        {
            using (var bytesBuilder = new MemoryStream())
            {
                using (var gzWriter = new GZipStream(bytesBuilder, CompressionLevel.Fastest, true))
                {
                    gzWriter.Write(data, 0, data.Length);
                    bytesBuilder.Flush();
                }

                return bytesBuilder.ToArray();
            }
        }

        /// <summary>
        /// 解压内容
        /// </summary>
        private byte[] Decompression(byte[] compressedData)
        {
            var buffer = new byte[compressedData.Length * 2];
            using (var inputStream = new MemoryStream(compressedData, 0, compressedData.Length))
            using (var gzReader = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var stream = new MemoryStream(compressedData.Length * 2))
            {
                var readBytes = 0;
                while ((readBytes = gzReader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, readBytes);
                }

                return stream.ToArray();
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
            encoding = encoding ?? DefaultEncoding;

            var compressedData = Decompression(obj);
            var data = encoding.GetString(compressedData);
            if (_serializerSettings == null)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data, _serializerSettings);
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
            encoding = encoding ?? DefaultEncoding;
            string data = "";
            if (_serializerSettings == null)
            {
                data = JsonConvert.SerializeObject(obj);
            }
            else
            {
                data = JsonConvert.SerializeObject(obj, _serializerSettings);
            }
            return Compression(encoding.GetBytes(data));
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(SerializeByte<T>(obj, encoding));
        }
    }
}
