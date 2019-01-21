using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// Redis String底层命令
    /// </summary>
    public static class StringCommands
    {
        /// <summary>
        /// 将value追加到key的值后，如果不存在则创建。
        /// </summary>
        /// <param name="key">字符串key</param>
        /// <param name="value">追加字符串</param>
        /// <returns></returns>
        public static ReturnTypeWithInt Append(string key, string value)
        {
            return new ReturnTypeWithInt("APPEND", key, value);
        }

        /// <summary>
        /// 计算给定字符串中被设置为1的比特位数量。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="start">起始位，可选</param>
        /// <param name="end">结束位，可选</param>
        /// <returns></returns>
        public static ReturnTypeWithInt BitCount(string key, long? start = null, long? end = null)
        {
            string[] args = start.HasValue && end.HasValue
                ? new[] { key, start.Value.ToString(), end.Value.ToString() }
                : new[] { key };
            return new ReturnTypeWithInt("BITCOUNT", args);
        }

        /// <summary>
        /// 对一个或多个保存二进制位的字符串key进行位元操作，并将结果保存到destkey上。
        /// </summary>
        /// <param name="operation">位元操作</param>
        /// <param name="destKey">目标key</param>
        /// <param name="keys">需要计算的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt BitOp(RedisBitOp operation, string destKey, params string[] keys)
        {
            return new ReturnTypeWithInt("BITOP", keys.Insert(destKey).Insert(operation.ToString().ToUpperInvariant()).ToArray());
        }

        /// <summary>
        /// 将key中储存的数字值减一。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt Decr(string key)
        {
            return new ReturnTypeWithInt("DECR", key);
        }

        /// <summary>
        /// 将key中储存的数字减decrement。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="decrement">需要减去的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt DecrBy(string key, long decrement)
        {
            return new ReturnTypeWithInt("DECRBY", key, decrement);
        }

        /// <summary>
        /// 返回key所关联的字符串值。
        /// </summary>
        /// <param name="key">需要获取的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString Get(string key)
        {
            return new ReturnTypeWithString("GET", key);
        }

        /// <summary>
        /// 获取key存储的字符串上指定偏移量上的位
        /// </summary>
        /// <param name="key">需要获取的key</param>
        /// <param name="offset">偏移量</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool GetBit(string key, uint offset)
        {
            return new ReturnTypeWithBool("GETBIT", key, offset);
        }

        /// <summary>
        /// 获取key中字符串的子字符串。
        /// </summary>
        /// <param name="key">需要获取的key</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString GetRange(string key, long start, long end)
        {
            return new ReturnTypeWithString("GETRANGE", key, start, end);
        }

        /// <summary>
        /// 将给定key的值设为value，并返回key的旧值。
        /// </summary>
        /// <param name="key">需要存储的key</param>
        /// <param name="value">需要存储的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString GetSet(string key, object value)
        {
            return new ReturnTypeWithString("GETSET", key, value);
        }

        /// <summary>
        /// 将key中存储的数字值增一。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt Incr(string key)
        {
            return new ReturnTypeWithInt("INCR", key);
        }

        /// <summary>
        /// 将key所存储的值加上increment。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="increment">需要增加的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt IncrBy(string key, long increment)
        {
            return new ReturnTypeWithInt("INCRBY", key, increment);
        }

        /// <summary>
        /// 将key所存储的值加上浮点数increment。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="increment">需要增加的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithFloat IncrByFloat(string key, double increment)
        {
            return new ReturnTypeWithFloat("INCRBYFLOAT", key, increment);
        }

        /// <summary>
        /// 返回所有给定key的值。
        /// </summary>
        /// <param name="keys">需要获取的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray MGet(params string[] keys)
        {
            return new ReturnTypeWithStringArray("MGET", keys);
        }

        /// <summary>
        /// 同时设置一个或多个key-value对。
        /// </summary>
        /// <param name="keyValues">需要增加的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus MSet(params string[] keyValues)
        {
            return new ReturnTypeWithStatus("MSET", keyValues);
        }

        /// <summary>
        /// 同时设置一个或多个key-value对，如果key存在所有设置失败。
        /// </summary>
        /// <param name="keyValues">需要增加的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool MSetNx(params string[] keyValues)
        {
            return new ReturnTypeWithBool("MSETNX", keyValues);
        }

        /// <summary>
        /// 设置指定key的值，且过期时间单位为毫秒
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="milliseconds">过期时间</param>
        /// <param name="value">设置的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus PSetEx(string key, long milliseconds, object value)
        {
            return new ReturnTypeWithStatus("PSETEX", key, milliseconds, value);
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
        public static ReturnTypeWithStatus Set(string key, object value, int? expirationSeconds = null, long? expirationMilliseconds = null, RedisExistence? exists = null)
        {
            var args = new List<string> { key, value.ToString() };
            if (expirationSeconds != null)
                args.AddRange(new[] { "EX", expirationSeconds.ToString() });
            if (expirationMilliseconds != null)
                args.AddRange(new[] { "PX", expirationMilliseconds.ToString() });
            if (exists != null)
                args.AddRange(new[] { exists.ToString().ToUpperInvariant() });
            var cmd = new ReturnTypeWithStatus("SET", args.ToArray());
            cmd.IsNullable = true;
            return cmd;
        }

        /// <summary>
        /// 对key所储存的字符串值设置或清除指定偏移量上的位
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReturnTypeWithBool SetBit(string key, uint offset, bool value)
        {
            return new ReturnTypeWithBool("SETBIT", key, offset, value ? "1" : "0");
        }

        /// <summary>
        /// 将值value设定到key。
        /// </summary>
        /// <param name="key">需要设定的key</param>
        /// <param name="seconds">过期时间，单位秒</param>
        /// <param name="value">设定的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus SetEx(string key, long seconds, object value)
        {
            return new ReturnTypeWithStatus("SETEX", key, seconds, value);
        }

        /// <summary>
        /// 将key的值设为value，当且仅当key不存在。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ReturnTypeWithBool SetNx(string key, object value)
        {
            return new ReturnTypeWithBool("SETNX", key, value);
        }

        /// <summary>
        /// 用value参数覆写给定key所储存的字符串值，从偏移量offset开始。
        /// </summary>
        /// <param name="key">需要覆写的key</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">覆写的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt SetRange(string key, uint offset, object value)
        {
            return new ReturnTypeWithInt("SETRANGE", key, offset, value);
        }

        /// <summary>
        /// 返回key所储存的字符串值的长度
        /// </summary>
        /// <param name="key">需要获取的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt StrLen(string key)
        {
            return new ReturnTypeWithInt("STRLEN", key);
        }
    }
}
