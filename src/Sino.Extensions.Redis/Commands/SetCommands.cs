using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Extensions.Redis.Commands
{
    /// <summary>
    /// Redis Set底层命令
    /// </summary>
    public static class SetCommands
    {
        /// <summary>
        /// 将一个或多个member元素加入到集合key当中，已经存在的集合的member元素将被忽略。
        /// </summary>
        /// <param name="key">集合key</param>
        /// <param name="members">元素</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt SAdd(string key, params object[] members)
        {
            return new ReturnTypeWithInt("SADD", key, members);
        }

        /// <summary>
        /// 返回集合key的基数（集合中元素的数量）
        /// </summary>
        /// <param name="key">集合key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt SCard(string key)
        {
            return new ReturnTypeWithInt("SCARD", key);
        }

        /// <summary>
        /// 返回一个集合的全部成员，该集合是所有给定集合之间的差集。
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static ReturnTypeWithStringArray SDiff(params string[] keys)
        {
            return new ReturnTypeWithStringArray("SDIFF", keys);
        }
    }
}
