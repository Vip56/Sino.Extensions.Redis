namespace Sino.Extensions.Redis
{
    public enum RedisObjectSubCommand
    {
        /// <summary>
        /// Return the number of references of the value associated with the specified key
        /// </summary>
        RefCount,

        /// <summary>
        /// Return the number of seconds since the object stored at the specified key is idle
        /// </summary>
        IdleTime,
    };
}
