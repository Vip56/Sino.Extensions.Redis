using Sino.CacheStore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public abstract class StoreHandler : IStoreHandler
    {
        public abstract Task<CacheStoreCommand<T>> Process<T>(CacheStoreCommand<T> command);
    }
}
