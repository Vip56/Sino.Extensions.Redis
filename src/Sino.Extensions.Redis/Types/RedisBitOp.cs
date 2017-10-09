namespace Sino.Extensions.Redis
{
    /// <summary>
    /// Operation used by Redis BITOP command
    /// </summary>
    public enum RedisBitOp
    {
        /// <summary>
        /// Bitwise AND
        /// </summary>
        And,

        /// <summary>
        /// Bitwise OR
        /// </summary>
        Or,

        /// <summary>
        /// Bitwise EXCLUSIVE-OR
        /// </summary>
        XOr,

        /// <summary>
        /// Bitwise NOT
        /// </summary>
        Not,
    }
}
