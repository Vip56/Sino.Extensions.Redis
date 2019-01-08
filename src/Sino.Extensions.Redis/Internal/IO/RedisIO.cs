using System;
using System.IO;
using System.Text;

namespace Sino.Extensions.Redis.Internal.IO
{
    public class RedisIO : IDisposable
    {
        readonly RedisWriter _writer;
        RedisReader _reader;
        BufferedStream _stream;

        public RedisWriter Writer { get { return _writer; } }
        public RedisReader Reader { get { return GetOrThrow(_reader); } }
        public Encoding Encoding { get; set; }
        public Stream Stream { get { return GetOrThrow(_stream); } }

        public RedisIO()
        {
            _writer = new RedisWriter(this);
            Encoding = new UTF8Encoding(false);
        }

        public void SetStream(Stream stream)
        {
            _stream?.Dispose();
            _stream = new BufferedStream(stream);
            _reader = new RedisReader(this);
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }

        static T GetOrThrow<T>(T obj)
        {
            if (obj == null)
                throw new RedisClientException("Connection was not opened");

            return obj;
        }
    }
}
