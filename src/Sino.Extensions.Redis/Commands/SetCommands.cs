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
        /// <param name="keys">集合key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray SDiff(params string[] keys)
        {
            return new ReturnTypeWithStringArray("SDIFF", keys);
        }

        /// <summary>
        /// 将指定集合keys的差集保存到集合destination中，如果destination存在
        /// 则覆盖。
        /// </summary>
        /// <param name="destination">目标保存集合</param>
        /// <param name="keys">计算集合</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt SDiffStore(string destination, params string[] keys)
        {
            return new ReturnTypeWithInt("SDIFFSTORE", destination, keys);
        }

        /// <summary>
        /// 返回指定集合keys的交集，如果key不存在则视为空集。
        /// </summary>
        /// <param name="keys">计算集合</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray SInter(params string[] keys)
        {
            return new ReturnTypeWithStringArray("SINTER", keys);
        }

        /// <summary>
        /// 将指定集合keys的交集保存到集合destination中，如果destination存在
        /// 则覆盖。
        /// </summary>
        /// <param name="destination">目标保存集合</param>
        /// <param name="keys">计算集合</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt SInterStore(string destination, params string[] keys)
        {
            return new ReturnTypeWithInt("SINTERSTORE", destination, keys);
        }

        /// <summary>
        /// 判断member元素是否是集合key的成员
        /// </summary>
        /// <param name="key">集合key</param>
        /// <param name="member">元素</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool SIsMember(string key, object member)
        {
            return new ReturnTypeWithBool("SISMEMBER", key, member);
        }

        /// <summary>
        /// 返回集合key中的所有成员
        /// </summary>
        /// <param name="key">集合key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray SMembers(string key)
        {
            return new ReturnTypeWithStringArray("SMEMBERS", key);
        }

        /// <summary>
        /// 将member元素从集合source移动到集合destination中。
        /// </summary>
        /// <param name="source">源集合</param>
        /// <param name="destination">目标集合</param>
        /// <param name="member">元素</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithBool SMove(string source, string destination, object member)
        {
            return new ReturnTypeWithBool("SMOVE", source, destination, member);
        }

        /// <summary>
        /// 移除并返回集合中的一个随机元素
        /// </summary>
        /// <param name="key">集合key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString SPop(string key)
        {
            return new ReturnTypeWithString("SPOP", key);
        }

        /// <summary>
        /// 随机返回集合中的一个元素
        /// </summary>
        /// <param name="key">集合key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithString SRandMember(string key)
        {
            return new ReturnTypeWithString("SRANDMEMBER", key);
        }

        /// <summary>
        /// 随机返回集合中count个元素，如果count为整数且小于集合基数
        /// 则返回count个不相同元素，如果count未负数则返回count个元素，
        /// 元素可能会重复出现多次。
        /// </summary>
        /// <param name="key">集合key</param>
        /// <param name="count">抽取元素个数</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray SRandMember(string key, long count)
        {
            return new ReturnTypeWithStringArray("SRANDMEMBER", key, count);
        }

        /// <summary>
        /// 移除集合key中一个或多个member元素，不存在的member元素会被忽略。
        /// </summary>
        /// <param name="key">集合key</param>
        /// <param name="members">移除的元素</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt SRem(string key, params object[] members)
        {
            return new ReturnTypeWithInt("SREM", key, members);
        }

        /// <summary>
        /// 返回一个集合的全部成员，该集合是所有给定集合的并集。
        /// </summary>
        /// <param name="keys">集合key</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithStringArray SUnion(params string[] keys)
        {
            return new ReturnTypeWithStringArray("SUNION", keys);
        }

        /// <summary>
        /// 将指定keys的集合的并集保存到destination集合中。
        /// </summary>
        /// <param name="destination">目标集合</param>
        /// <param name="keys">计算集合</param>
        /// <returns>命令对象</returns>
        public static ReturnTypeWithInt SUnionStore(string destination, params string[] keys)
        {
            return new ReturnTypeWithInt("SUNIONSTORE", destination, keys);
        }
    }
}
