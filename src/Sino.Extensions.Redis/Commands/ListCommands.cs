using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// Redis List底层命令
    /// </summary>
    public static class ListCommands
    {
        /// <summary>
        /// 采用阻塞模式获取指定key数组中列表的值，如果指定keys任意
        /// 列表存在一条数据则返回，否则将等待直到指定timeout超时。
        /// </summary>
        /// <param name="timeout">超时时间，单位为秒，0代表一直等待。</param>
        /// <param name="keys">需要查询的keys</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithTuple BLPop(int timeout, params string[] keys)
        {
            return new ReturnTypeWithTuple("BLPOP", keys, timeout);
        }

        /// <summary>
        /// 采用阻塞模式获取指定key数组中列表末尾值，如果指定keys任意
        /// 列表存在一条数据则返回，否则将等待直到指定timeout超时。
        /// </summary>
        /// <param name="timeout">超时时间，单位为秒，0代表一直等待。</param>
        /// <param name="keys">需要查询的keys</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithTuple BRPop(int timeout, params string[] keys)
        {
            return new ReturnTypeWithTuple("BRPOP", keys, timeout);
        }

        /// <summary>
        /// 采用阻塞模式获取source列表中的末尾元素，返回的同时将其添加
        /// 到destination的头部。如果source不存在任何数据则等待，直到
        /// 指定的timeout超时。
        /// </summary>
        /// <param name="source">源key</param>
        /// <param name="destination">目标key</param>
        /// <param name="timeout">超时时间，单位为秒，0代表一直等待。</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString BRPopLPush(string source, string destination, int timeout)
        {
            var cmd = new ReturnTypeWithString("BRPOPLPUSH", source, destination, timeout);
            cmd.IsNullable = true;
            return cmd;
        }

        /// <summary>
        /// 返回列表key中指定下标的元素。
        /// </summary>
        /// <param name="key">需要查询的列表key</param>
        /// <param name="index">下标，从0开始</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString LIndex(string key, long index)
        {
            return new ReturnTypeWithString("LINDEX", key, index);
        }

        /// <summary>
        /// 将值value插入到列表key当中，位于值pivot之后或之前。如果key或pivot
        /// 不存在，则不执行任何操作。
        /// </summary>
        /// <param name="key">需要插入的key</param>
        /// <param name="insertType">插入顺序</param>
        /// <param name="pivot">位置值</param>
        /// <param name="value">插入的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt LInsert(string key, RedisInsert insertType, string pivot, object value)
        {
            return new ReturnTypeWithInt("LINSERT", key, insertType.ToString().ToUpperInvariant(), pivot, value);
        }

        /// <summary>
        /// 返回列表key的长度。
        /// </summary>
        /// <param name="key">查询的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt LLen(string key)
        {
            return new ReturnTypeWithInt("LLEN", key);
        }


    }
}
