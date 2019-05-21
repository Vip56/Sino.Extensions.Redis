using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public static class ListCommands
    {
        /// <summary>
        /// 采用阻塞模式获取指定key数组中列表的值，如果指定keys任意
        /// 列表存在一条数据则返回，否则将等待直到指定timeout超时。
        /// </summary>
        /// <param name="timeout">超时时间，单位为秒，0代表一直等待。</param>
        /// <param name="keys">需要查询的keys</param>
        /// <returns>命令对象</returns>
        public static ResultWithTuple BLPop(int timeout, params string[] keys)
        {
            return new ResultWithTuple("BLPOP", keys.Append(timeout.ToString()).ToArray());
        }

        /// <summary>
        /// 移除并返回列表key的头元素
        /// </summary>
        /// <param name="key">列表key</param>
        /// <returns>命令对象</returns>
        public static ResultWithString LPop(string key)
        {
            return new ResultWithString("LPOP", key);
        }

        /// <summary>
        /// 采用阻塞模式获取指定key数组中列表末尾值，如果指定keys任意
        /// 列表存在一条数据则返回，否则将等待直到指定timeout超时。
        /// </summary>
        /// <param name="timeout">超时时间，单位为秒，0代表一直等待。</param>
        /// <param name="keys">需要查询的keys</param>
        /// <returns>命令对象</returns>
        public static ResultWithTuple BRPop(int timeout, params string[] keys)
        {
            return new ResultWithTuple("BRPOP", keys.Append(timeout.ToString()).ToArray());
        }

        /// <summary>
        /// 返回列表key中指定下标的元素。
        /// </summary>
        /// <param name="key">需要查询的列表key</param>
        /// <param name="index">下标，从0开始</param>
        /// <returns>命令对象</returns>
        public static ResultWithString LIndex(string key, long index)
        {
            return new ResultWithString("LINDEX", key, index);
        }

        /// <summary>
        /// 返回列表key的长度。
        /// </summary>
        /// <param name="key">查询的key</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt LLen(string key)
        {
            return new ResultWithInt("LLEN", key);
        }

        /// <summary>
        /// 将一个或多个值插入到列表key的表头
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="values">插入的值</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt LPush(string key, params object[] values)
        {
            return new ResultWithInt("LPUSH", values.Insert(key).ToArray());
        }

        /// <summary>
        /// 移除并返回列表key的尾元素
        /// </summary>
        /// <param name="key">列表key</param>
        /// <returns>命令对象</returns>
        public static ResultWithString RPop(string key)
        {
            return new ResultWithString("RPOP", key);
        }

        /// <summary>
        /// 将一个或多个值value插入到列表key的尾巴。
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="values">添加的值</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt RPush(string key, params object[] values)
        {
            return new ResultWithInt("RPUSH", values.Insert(key).ToArray());
        }
    }
}
