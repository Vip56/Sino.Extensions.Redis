using Newtonsoft.Json;
using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Serializer.Json
{
    public class GzJsonConvertProvider : ConvertProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public GzJsonConvertProvider(Encoding encoding, JsonSerializerSettings settings)
            :base(encoding)
        {
            _serializerSettings = settings;
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

        public override byte[] SerializeByte<T>(T obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public override Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }
    }
}
