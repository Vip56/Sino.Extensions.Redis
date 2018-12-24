using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// Redis Hash底层命令
    /// </summary>
    public static class HashCommands
    {
        /// <summary>
        /// 删除哈希表key中的一个或多个指定域，不存在的域将被忽略。
        /// </summary>
        /// <param name="key">需要删除的key</param>
        /// <param name="fields">需要删除的域数组</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt HDel(string key, params string[] fields)
        {
            return new ReturnTypeWithInt("HDEL", key, fields);
        }

        /// <summary>
        /// 查看哈希表key中给定域field是否存在
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <param name="field">需要查询的域</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool HExists(string key, string field)
        {
            return new ReturnTypeWithBool("HEXISTS", key, field);
        }

        /// <summary>
        /// 返回哈希表key中给定域的值
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <param name="field">需要查询的域</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString HGet(string key, string field)
        {
            return new ReturnTypeWithString("HGET", key, field);
        }

        /// <summary>
        /// 返回哈希表key中所有的域和值
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithHash HGetAll(string key)
        {
            return new ReturnTypeWithHash("HGETALL", key);
        }

        /// <summary>
        /// 为哈希表key中的域field的值加上增量increment。增量也可以为负数，
        /// 相当于对给定域进行减法操作。如果key或域不存在，那么在执行命令前，
        /// 将会自动创建key和域，且域的值被初始化为0。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="field">需要计算的域</param>
        /// <param name="increment">增量值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt HIncrBy(string key, string field, long increment)
        {
            return new ReturnTypeWithInt("HINCRBY", key, field, increment);
        }

        /// <summary>
        /// 为哈希表key中的域field的值加上增量increment。如果key或域不存在，
        /// 那么在执行命令前将会创建key和域，且域默认初始化为0。
        /// </summary>
        /// <param name="key">需要计算的key</param>
        /// <param name="field">需要计算的域</param>
        /// <param name="increment">增量值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithFloat HIncrByFloat(string key, string field, double increment)
        {
            return new ReturnTypeWithFloat("HINCRBYFLOAT", key, field, increment);
        }

        /// <summary>
        /// 返回哈希表key中的所有域。
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray HKeys(string key)
        {
            return new ReturnTypeWithStringArray("HKEYS", key);
        }

        /// <summary>
        /// 返回哈希表key中域的数量。
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt HLen(string key)
        {
            return new ReturnTypeWithInt("HLEN", key);
        }

        /// <summary>
        /// 返回哈希表key中一个或多个给定域的值。
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <param name="fields">需要查询的域数组</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray HMGet(string key, params string[] fields)
        {
            return new ReturnTypeWithStringArray("HMGET", key, fields);
        }

        /// <summary>
        /// 同时设置多个域的值到哈希表的key中。
        /// </summary>
        /// <param name="key">需要保存的key</param>
        /// <param name="dict">需要保存的域和值字典</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStatus HMSet(string key, Dictionary<string, string> dict)
        {
            var args = dict.Select(x => string.Concat(x.Key, " ", x.Value));
            return new ReturnTypeWithStatus("HMSET", key, args.ToArray());
        }

        /// <summary>
        /// 将哈希表key中的域的值设置为value。如果key和域不存在
        /// 则会自动创建，如果域已经存在，旧值将会覆盖。
        /// </summary>
        /// <param name="key">需要保存的key</param>
        /// <param name="field">需要保存的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool HSet(string key, string field, object value)
        {
            return new ReturnTypeWithBool("HSET", key, field, value);
        }

        /// <summary>
        /// 将哈希表key中的域的值设置为value,当且仅当域field不存在。
        /// </summary>
        /// <param name="key">需要保存的key</param>
        /// <param name="field">需要保存的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool HSetNx(string key, string field, object value)
        {
            return new ReturnTypeWithBool("HSETNX", key, field, value);
        }
    }
}
