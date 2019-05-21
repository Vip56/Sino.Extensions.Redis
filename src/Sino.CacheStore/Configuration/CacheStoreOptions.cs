using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Configuration
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class CacheStoreOptions : IOptions<CacheStoreOptions>
    {
        /// <summary>
        /// Redis配置
        /// </summary>
        public CacheStoreWithRedis Redis { get; set; }

        CacheStoreOptions IOptions<CacheStoreOptions>.Value
        {
            get { return this; }
        }
    }
}
