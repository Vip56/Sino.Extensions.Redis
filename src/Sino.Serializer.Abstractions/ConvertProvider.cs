using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Serializer.Abstractions
{
    /// <summary>
    /// 序列化标准接口抽象类
    /// </summary>
    public abstract class ConvertProvider : IConvertProvider
    {
        /// <summary>
        /// 默认编码格式
        /// </summary>
        protected Encoding DefaultEncoding { get; set; }

        public ConvertProvider(Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding));

            DefaultEncoding = encoding;
        }

        public abstract T Deserialize<T>(string obj, Encoding encoding = null);

        public abstract Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null);

        public abstract T DeserializeByte<T>(byte[] obj, Encoding encoding = null);

        public abstract Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null);

        public abstract string Serialize<T>(T obj, Encoding encoding = null);

        public abstract Task<string> SerializeAsync<T>(T obj, Encoding encoding = null);

        public abstract byte[] SerializeByte<T>(T obj, Encoding encoding = null);

        public abstract Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null);
    }
}
