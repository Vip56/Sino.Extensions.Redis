using Sino.CacheStore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public interface ICacheStoreHandler
    {
        Task Init();
        Task<CacheStoreCommand<T>> ProcessAsync<T>(CacheStoreCommand<T> command);
    }
}
