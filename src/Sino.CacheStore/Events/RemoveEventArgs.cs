using System;

namespace Sino.CacheStore.Events
{
    /// <summary>
    /// 删除类事件
    /// </summary>
    public sealed class RemoveEventArgs : EventArgs
    {
        /// <summary>
        /// 创建事件对象
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="opType">操作方式</param>
        /// <param name="command">具体命令</param>
        public RemoveEventArgs(string key, OperatorType opType, string command)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            Key = key;
            Type = opType;
            Command = command;
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 操作方式
        /// </summary>
        public OperatorType Type { get; }

        /// <summary>
        /// 具体命令
        /// </summary>
        public string Command { get; }
    }
}
