using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// Redis Key底层命令
    /// </summary>
    public static class KeyCommands
    {
        /// <summary>
        /// 删除给定的一个或多个key，不存在的key将会忽略。
        /// </summary>
        /// <param name="keys">需要删除的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt Del(params string[] keys)
        {
            return new ReturnTypeWithInt("DEL", keys);
        }

        /// <summary>
        /// 序列化给定key，其中序列化的值不包含任何生存时间信息。
        /// </summary>
        /// <param name="key">需要序列化的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBytes Dump(string key)
        {
            return new ReturnTypeWithBytes("DUMP", key);
        }

        /// <summary>
        /// 检查给定key是否存在
        /// </summary>
        /// <param name="key">需要判断的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool Exists(string key)
        {
            return new ReturnTypeWithBool("EXISTS", key);
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
        public static ReturnTypeWithBool Expire(string key, int seconds)
        {
            return new ReturnTypeWithBool("EXPIRE", key, seconds);
        }

        /// <summary>
        /// 为给定key设置生存时间，当key过期时，他会被自动删除。
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="timestamp">指定超时日期的UNIX时间戳，精确到秒</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool ExpireAt(string key, long timestamp)
        {
            return new ReturnTypeWithBool("EXPIREAT", key, timestamp);
        }

        /// <summary>
        /// 查找所有符合给定模式pattern的key。
        /// </summary>
        /// <param name="pattern">匹配模式，如*匹配所有，h?llo匹配hello和hxllo等</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray Keys(string pattern)
        {
            return new ReturnTypeWithStringArray("KEYS", pattern);
        }

        /// <summary>
        /// 将指定key迁移到其他实例，迁移成功后删除本实例。
        /// </summary>
        /// <param name="host">地址</param>
        /// <param name="port">端口</param>
        /// <param name="key">对方实例key</param>
        /// <param name="destinationDb">对方实例数据库序号</param>
        /// <param name="timeoutMilliseconds">超时时间，单位为毫秒</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus Migrate(string host, int port, string key, int destinationDb, int timeoutMilliseconds)
        {
            return new ReturnTypeWithStatus("MIGRATE", host, port, key, destinationDb, timeoutMilliseconds);
        }

        /// <summary>
        /// 将当前数据库的key移动到给定的数据库db当中。
        /// </summary>
        /// <param name="key">需要移动的key</param>
        /// <param name="database">数据库序号</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool Move(string key, int database)
        {
            return new ReturnTypeWithBool("MOVE", key, database);
        }

        /// <summary>
        /// 获取给定key自储存以来的空闲时间，没有被读取也没有被写入，以秒为单位。
        /// </summary>
        /// <param name="key">需要查找的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt ObjectIdleTime(string key)
        {
            return new ReturnTypeWithInt("OBJECT", "IDLETIME", key);
        }

        /// <summary>
        /// 移除给定key的生存时间，将这个key设置为永久保存。
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool Persist(string key)
        {
            return new ReturnTypeWithBool("PERSIST", key);
        }

        /// <summary>
        /// 为给定key设置生存时间，当key过期时，它会被自动删除。
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="milliseconds">超时时间，单位为毫秒</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool PExpire(string key, long milliseconds)
        {
            return new ReturnTypeWithBool("PEXPIRE", key, milliseconds);
        }

        /// <summary>
        /// 为给定key设置生存时间，当key过期时，他会被自动删除。
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="timestamp">指定超时日期的UNIX时间戳，精确到毫秒</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool PExpireAt(string key, long timestamp)
        {
            return new ReturnTypeWithBool("PEXPIREAT", key, timestamp);
        }

        /// <summary>
        /// 查询给定key的剩余生存时间，单位以毫秒为单位。
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt PTtl(string key)
        {
            return new ReturnTypeWithInt("PTTL", key);
        }

        /// <summary>
        /// 从当前数据库中随机返回（不删除）一个key。
        /// </summary>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString RandomKey()
        {
            return new ReturnTypeWithString("RANDOMKEY");
        }

        /// <summary>
        /// 将key更名为newkey，当key与newkey相同或key不存在返回错误，
        /// 如果newkey在当前数据库存在则覆盖旧值。
        /// </summary>
        /// <param name="key">需要更新的旧key</param>
        /// <param name="newKey">新的key名</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus Rename(string key, string newKey)
        {
            return new ReturnTypeWithStatus("RENAME", key, newKey);
        }

        /// <summary>
        /// 当且newkey不存在时，将key改名为newkey。
        /// </summary>
        /// <param name="key">需要更新的旧key</param>
        /// <param name="newKey">新的key名</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool RenameNx(string key, string newKey)
        {
            return new ReturnTypeWithBool("RENAMENX", key, newKey);
        }

        /// <summary>
        /// 反序列化给定的序列化值，并将它和给定的
        /// </summary>
        /// <param name="key">存储的key</param>
        /// <param name="ttl">超时时间，以毫秒为单位</param>
        /// <param name="serializedValue">通过<see cref="Dump(string)"/>获取的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus Restore(string key, long ttl, string serializedValue)
        {
            return new ReturnTypeWithStatus("RESTORE", key, ttl, serializedValue);
        }

        /// <summary>
        /// 以秒为单位，返回给定key的剩余生存时间。
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt Ttl(string key)
        {
            return new ReturnTypeWithInt("TTL", key);
        }
    }
}
