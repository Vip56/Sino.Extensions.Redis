using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public static class KeyCommands
    {
        /// <summary>
        /// 检查给定key是否存在
        /// </summary>
        /// <param name="key">需要判断的key</param>
        /// <returns>命令对象</returns>
        public static ResultWithBool Exists(string key)
        {
            return new ResultWithBool("EXISTS", key);
        }

        /// <summary>
        /// 返回key所关联的字符串值。
        /// </summary>
        /// <param name="key">需要获取的key</param>
        /// <returns>命令对象</returns>
        public static ResultWithString Get(string key)
        {
            return new ResultWithString("GET", key);
        }

        /// <summary>
        /// 设置指定key的值
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="value">需要设置的值</param>
        /// <param name="expirationSeconds">过期时间（秒）</param>
        /// <param name="expirationMilliseconds">过期时间（毫秒）</param>
        /// <param name="exists">其他限定条件</param>
        /// <returns>命令对象</returns>
        public static ResultWithStatus Set(string key, object value, int? expirationSeconds = null, long? expirationMilliseconds = null, RedisExistence? exists = null)
        {
            var args = new List<string> { key, value.ToString() };
            if (expirationSeconds != null)
                args.AddRange(new[] { "EX", expirationSeconds.ToString() });
            if (expirationMilliseconds != null)
                args.AddRange(new[] { "PX", expirationMilliseconds.ToString() });
            if (exists != null)
                args.AddRange(new[] { exists.ToString().ToUpperInvariant() });
            var cmd = new ResultWithStatus("SET", args.ToArray());
            cmd.IsNullable = true;
            return cmd;
        }

        /// <summary>
        /// 为给定key设置生存时间，当key过期时，它会被自动删除。
        /// </summary>
        /// <remarks>
        /// 在Redis 2.4版本中，过期时间的延迟在1秒钟之内，也就是说
        /// key已经过期，但它还是可能在过期之后一秒中之内被访问到，
        /// 而在新的Redis 2.6版本中，延迟降低到1毫秒之内。
        /// </remarks>
        /// <param name="key">需要设置的key</param>
        /// <param name="seconds">设置的时间，单位为秒</param>
        /// <returns>命令对象</returns>
        public static ResultWithBool Expire(string key, int seconds)
        {
            return new ResultWithBool("EXPIRE", key, seconds);
        }

        /// <summary>
        /// 为给定key设置生存时间，当key过期时，它会被自动删除。
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="milliseconds">超时时间，单位为毫秒</param>
        /// <returns>命令对象</returns>
        public static ResultWithBool PExpire(string key, long milliseconds)
        {
            return new ResultWithBool("PEXPIRE", key, milliseconds);
        }

        /// <summary>
        /// 删除给定的一个或多个key，不存在的key将会忽略。
        /// </summary>
        /// <param name="keys">需要删除的key</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt Remove(params string[] keys)
        {
            return new ResultWithInt("DEL", keys);
        }
    }
}
