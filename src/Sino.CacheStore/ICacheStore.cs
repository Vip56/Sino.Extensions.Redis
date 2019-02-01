using Sino.CacheStore.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore
{
    /// <summary>
    /// 缓存存储接口
    /// </summary>
    public interface ICacheStore : ICacheStoreEvent
    {
        string Get(string key);


        Task<string> GetAsync(string key);


    }
}
