using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis
{
    /// <summary>
    /// Redis缓存接口
    /// </summary>
    public interface IRedisCache
    {
        /// <summary>
        /// 返回Key所关联的字符串
        /// </summary>
        string Get(string key);

        /// <summary>
        /// 返回Key所关联的字符串
        /// </summary>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 重新设置Key的超时时间
        /// </summary>
        /// <param name="timeout">超时时间，单位秒</param>
        void Refresh(string key, int timeout);

        /// <summary>
        /// 重新设置key的超时时间
        /// </summary>
        /// <param name="timeout">超时时间，单位秒</param>
        Task RefreshAsync(string key, int timeout);

        /// <summary>
        /// 移除Key
        /// </summary>
        void Remove(string key);

        /// <summary>
        /// 移除Key
        /// </summary>
        Task RemoveAsync(string key);
        
        /// <summary>
        /// 设置Key的值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="timeout">超时时间，单位秒</param>
        void Set(string key, string value, int? timeout = null);

        /// <summary>
        /// 设置Key值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="timeout">超时时间，单位秒</param>
        Task SetAsync(string key, string value, int? timeout = null);

        /// <summary>
        /// 将value追加到key值后，不存在则等同set
        /// </summary>
        /// <param name="value">需要追加的值</param>
        /// <returns>追加后key的字符长度</returns>
        long Append(string key, string value);

        /// <summary>
        /// 将value追加到key值后，不存在则等同set
        /// </summary>
        /// <param name="value">需要追加的值</param>
        /// <returns>追加后key的字符长度</returns>
        Task<long> AppendAsync(string key, string value);

        /// <summary>
        /// 返回key所存储的字符长度
        /// </summary>
        /// <returns>字符长度</returns>
        long StrLen(string key);

        /// <summary>
        /// 返回key所存储的字符长度
        /// </summary>
        /// <returns>字符长度</returns>
        Task<long> StrLenAsync(string key);

        /// <summary>
        /// 获取key字符串中子字符串
        /// </summary>
        /// <param name="start">起始位</param>
        /// <param name="end">结束位</param>
        /// <returns>子字符串</returns>
        string GetRange(string key, long start, long end);

        /// <summary>
        /// 获取key字符串中子字符串
        /// </summary>
        /// <param name="start">起始位</param>
        /// <param name="end">结束位</param>
        /// <returns>子字符串</returns>
        Task<string> GetRangeAsync(string key, long start, long end);


    }
}
