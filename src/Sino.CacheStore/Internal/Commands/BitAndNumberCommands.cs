using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public static class BitAndNumberCommands
    {
        /// <summary>
        /// 计算给定字符串中被设置为1的比特位数量。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="start">起始位，可选</param>
        /// <param name="end">结束位，可选</param>
        /// <returns></returns>
        public static ResultWithInt BitCount(string key, long? start = null, long? end = null)
        {
            string[] args = start.HasValue && end.HasValue
                ? new[] { key, start.Value.ToString(), end.Value.ToString() }
                : new[] { key };
            return new ResultWithInt("BITCOUNT", args);
        }

        /// <summary>
        /// 对key所储存的字符串值设置或清除指定偏移量上的位
        /// </summary>
        /// <param name="key">需要设置的key</param>
        /// <param name="offset"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ResultWithBool SetBit(string key, uint offset, bool value)
        {
            return new ResultWithBool("SETBIT", key, offset, value ? "1" : "0");
        }

        /// <summary>
        /// 获取key存储的字符串上指定偏移量上的位
        /// </summary>
        /// <param name="key">需要获取的key</param>
        /// <param name="offset">偏移量</param>
        /// <returns>命令对象</returns>
        public static ResultWithBool GetBit(string key, uint offset)
        {
            return new ResultWithBool("GETBIT", key, offset);
        }

        /// <summary>
        /// 将key中储存的数字值减一。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt Decr(string key)
        {
            return new ResultWithInt("DECR", key);
        }

        /// <summary>
        /// 将key中储存的数字减decrement。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="decrement">需要减去的值</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt DecrBy(string key, long decrement)
        {
            return new ResultWithInt("DECRBY", key, decrement);
        }

        /// <summary>
        /// 将key中存储的数字值增一。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt Incr(string key)
        {
            return new ResultWithInt("INCR", key);
        }

        /// <summary>
        /// 将key所存储的值加上increment。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="increment">需要增加的值</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt IncrBy(string key, long increment)
        {
            return new ResultWithInt("INCRBY", key, increment);
        }
    }
}
