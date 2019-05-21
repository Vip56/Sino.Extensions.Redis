using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore
{
    public class CacheStoreProtocolException : Exception
    {
        public CacheStoreProtocolException(string message)
            : base(message) { }
    }
}
