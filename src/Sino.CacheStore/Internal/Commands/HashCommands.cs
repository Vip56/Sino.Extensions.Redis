using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sino.CacheStore.Internal.Commands
{
    public static class HashCommands
    {
        /// <summary>
        /// 删除哈希表key中的一个或多个指定域，不存在的域将被忽略。
        /// </summary>
        /// <param name="key">需要删除的key</param>
        /// <param name="fields">需要删除的域数组</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt HDel(string key, params string[] fields)
        {
            return new ResultWithInt("HDEL", fields.Insert(key).ToArray());
        }

        /// <summary>
        /// 查看哈希表key中给定域field是否存在
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <param name="field">需要查询的域</param>
        /// <returns>命令对象</returns>
        public static ResultWithBool HExists(string key, string field)
        {
            return new ResultWithBool("HEXISTS", key, field);
        }

        /// <summary>
        /// 返回哈希表key中给定域的值
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <param name="field">需要查询的域</param>
        /// <returns>命令对象</returns>
        public static ResultWithString HGet(string key, string field)
        {
            return new ResultWithString("HGET", key, field);
        }

        /// <summary>
        /// 返回哈希表key中域的数量。
        /// </summary>
        /// <param name="key">需要查询的key</param>
        /// <returns>命令对象</returns>
        public static ResultWithInt HLen(string key)
        {
            return new ResultWithInt("HLEN", key);
        }

        /// <summary>
        /// 将哈希表key中的域的值设置为value。如果key和域不存在
        /// 则会自动创建，如果域已经存在，旧值将会覆盖。
        /// </summary>
        /// <param name="key">需要保存的key</param>
        /// <param name="field">需要保存的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>命令对象</returns>
        public static ResultWithBool HSet(string key, string field, object value)
        {
            return new ResultWithBool("HSET", key, field, value);
        }

        /// <summary>
        /// 将哈希表key中的域的值设置为value,当且仅当域field不存在。
        /// </summary>
        /// <param name="key">需要保存的key</param>
        /// <param name="field">需要保存的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>命令对象</returns>
        public static ResultWithBool HSetWithNoExisted(string key, string field, object value)
        {
            return new ResultWithBool("HSETNX", key, field, value);
        }
    }
}
