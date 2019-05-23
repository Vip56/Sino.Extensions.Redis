using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Events
{
    public enum OperatorType
    {
        /// <summary>
        /// 简单操作
        /// </summary>
        Normal,
        /// <summary>
        /// 列表操作
        /// </summary>
        List,
        /// <summary>
        /// 哈希表操作
        /// </summary>
        Hash,
        /// <summary>
        /// 按位或数值操作
        /// </summary>
        BitAndNumber,
        /// <summary>
        /// 其他操作
        /// </summary>
        Other = -1
    }
}
