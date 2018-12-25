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

        /// <summary>
        /// 移除并返回列表key的头元素
        /// </summary>
        /// <param name="key">列表key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString LPop(string key)
        {
            return new ReturnTypeWithString("LPOP", key);
        }

        /// <summary>
        /// 将一个或多个值插入到列表key的表头
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="values">插入的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt LPush(string key, params object[] values)
        {
            return new ReturnTypeWithInt("LPUSH", key, values);
        }

        /// <summary>
        /// 将value插入到列表key的表头，当且仅当key存在并且是一个列表
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="value">插入的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt LPushX(string key, object value)
        {
            return new ReturnTypeWithInt("LPUSHX", key, value);
        }

        /// <summary>
        /// 返回列表key中指定区间内的元素，区间以偏移量start和stop指定。
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="start">起始偏移</param>
        /// <param name="stop">结束偏移</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray LRange(string key, long start, long stop)
        {
            return new ReturnTypeWithStringArray("LRANGE", key, start, stop);
        }
        
        /// <summary>
        /// 移除列表key中count个与参数value相等的元素，如果count大于0 则
        /// 从表头开始向表尾搜索，如果count小于0则从表尾向表头搜索，count
        /// 为0则移除所有。
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="count">移除方向与个数</param>
        /// <param name="value">移除的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt LRem(string key, long count, object value)
        {
            return new ReturnTypeWithInt("LREM", key, count, value);
        }

        /// <summary>
        /// 将列表key下标为index的元素的值设置为value，如果下标超出或列表不存在则
        /// 异常。
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="index">下标</param>
        /// <param name="value">替换的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus LSet(string key, long index, object value)
        {
            return new ReturnTypeWithStatus("LSET", key, index, value);
        }

        /// <summary>
        /// 将列表key只保留指定区间内的元素，其他元素移除。
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="start">起始偏移</param>
        /// <param name="stop">结束偏移</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus LTrim(string key, long start, long stop)
        {
            return new ReturnTypeWithStatus("LTRIM", key, start, stop);
        }

        /// <summary>
        /// 移除并返回列表key的尾元素
        /// </summary>
        /// <param name="key">列表key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString RPop(string key)
        {
            return new ReturnTypeWithString("RPOP", key);
        }

        /// <summary>
        /// 将列表source的最后一个元素弹出并返回，同时将这个元素
        /// 添加到destination列表的头元素。
        /// </summary>
        /// <param name="source">源列表key</param>
        /// <param name="destination">目标列表key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString RPopLPush(string source, string destination)
        {
            return new ReturnTypeWithString("RPOPLPUSH", source, destination);
        }

        /// <summary>
        /// 将一个或多个值value插入到列表key的尾巴。
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="values">添加的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt RPush(string key, params object[] values)
        {
            return new ReturnTypeWithInt("RPUSH", key, values);
        }

        /// <summary>
        /// 将值value插入到列表key的尾巴，当且仅当key存在并且是一个列表
        /// </summary>
        /// <param name="key">列表key</param>
        /// <param name="value">添加的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt RPushX(string key, object value)
        {
            return new ReturnTypeWithInt("RPUSHX", key, value);
        }
    }
}
