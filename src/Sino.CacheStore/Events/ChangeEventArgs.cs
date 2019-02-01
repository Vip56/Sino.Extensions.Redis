using System;

namespace Sino.CacheStore.Events
{
    /// <summary>
    /// 修改类事件参数
    /// </summary>
    public sealed class ChangeEventArgs
    {
        /// <summary>
        /// 创建事件对象
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="opType">操作方式</param>
        /// <param name="command">具体命令</param>
        public ChangeEventArgs(string key, OperatorType opType, string command)
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
