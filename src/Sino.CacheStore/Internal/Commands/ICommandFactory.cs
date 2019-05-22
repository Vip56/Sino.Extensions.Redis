using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    /// <summary>
    /// 命令创建工厂接口
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// 存储方式名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 命令创建事件
        /// </summary>
        event EventHandler<CacheStoreCommand> OnCreateCommand;
    }
}
