using Bond;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.Bond
{
    /// <summary>
    /// 缓存基础类
    /// </summary>
    [Schema]
    public class BondCacheItem
    {
        public BondCacheItem() { }

        [Id(1)]
        public string Key { get; set; }

        [Id(2)]
        public string Value { get; set; }
    }
}
