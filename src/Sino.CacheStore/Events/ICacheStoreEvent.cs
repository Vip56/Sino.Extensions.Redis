using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Events
{
    /// <summary>
    /// 操作事件
    /// </summary>
    public interface ICacheStoreEvent
    {
        /// <summary>
        /// 查询类事件
        /// </summary>
        event EventHandler<QueryEventArgs> OnQuery;

        /// <summary>
        /// 修改类事件
        /// </summary>
        event EventHandler<ChangeEventArgs> OnChange;

        /// <summary>
        /// 删除类事件
        /// </summary>
        event EventHandler<RemoveEventArgs> OnRemove;
    }
}
