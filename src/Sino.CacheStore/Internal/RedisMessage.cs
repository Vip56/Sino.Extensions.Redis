namespace Sino.CacheStore.Internal
{
    /// <summary>
    /// Redis消息前缀
    /// </summary>
    public enum RedisMessage
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        Error = '-',

        /// <summary>
        /// 状态消息
        /// </summary>
        Status = '+',

        /// <summary>
        /// 批量消息
        /// </summary>
        Bulk = '$',

        /// <summary>
        /// 多个批量消息
        /// </summary>
        MultiBulk = '*',

        /// <summary>
        /// 数值消息
        /// </summary>
        Int = ':',
    }
}
