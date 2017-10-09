namespace Sino.Extensions.Redis
{
    /// <summary>
    /// Redis existence specification for SET command
    /// </summary>
    public enum RedisExistence
    {
        /// <summary>
        /// Only set the key if it does not already exist
        /// </summary>
        Nx,

        /// <summary>
        /// Only set the key if it already exists
        /// </summary>
        Xx,
    }
}
