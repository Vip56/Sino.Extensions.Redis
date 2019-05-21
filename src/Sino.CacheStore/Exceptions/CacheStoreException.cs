using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore
{
    public class CacheStoreException : Exception
    {
        public CacheStoreException(string message)
            : base(message) { }
    }
}
