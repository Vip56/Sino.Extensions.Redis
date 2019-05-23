using Sino.CacheStore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public abstract class StoreHandler : IStoreHandler
    {
        public virtual Task Init()
        {
            return Task.CompletedTask;
        }

        public abstract Task<CacheStoreCommand<T>> ProcessAsync<T>(CacheStoreCommand<T> command);
    }
}
