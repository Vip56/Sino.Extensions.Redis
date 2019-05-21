using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class RedisBinaryWriter
    {
        const char Bulk = (char)RedisMessage.Bulk;
        const char MultiBulk = (char)RedisMessage.MultiBulk;
        const string BOL = "\r\n";

        readonly Encoding _encoding;

        public RedisBinaryWriter(Encoding encoding)
        {
            _encoding = encoding;
        }


    }
}
