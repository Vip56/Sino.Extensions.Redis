using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Extensions.Redis
{
    public static class Ext
    {
        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetUnixTime(this DateTime dt)
        {
            var ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds * 1000);
        }

        /// <summary>
        /// 添加一个新的元素到头部
        /// </summary>
        /// <param name="element">新元素</param>
        /// <returns>添加新元素后的数组</returns>
        public static IEnumerable<TSource> Insert<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            var list = new List<TSource>();
            list.Add(element);
            list.AddRange(source);
            return list;
        }

        /// <summary>
        /// 添加一个新的元素到末尾
        /// </summary>
        /// <param name="element">新元素</param>
        /// <returns>添加新元素后的数组</returns>
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            var list = new List<TSource>();
            list.AddRange(source);
            list.Add(element);
            return list;
        }

        /// <summary>
        /// 将字典生成为key、value数组
        /// </summary>
        /// <returns>生成的数组</returns>
        public static string[] ToKVArray(this Dictionary<string, string> dict)
        {
            var list = new List<string>();
            foreach(var kv in dict)
            {
                list.Add(kv.Key);
                list.Add(kv.Value);
            }
            return list.ToArray();
        }
    }
}
