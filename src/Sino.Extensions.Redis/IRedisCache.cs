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
        /// 判断key是否存在
        /// </summary>
        bool Exists(string key);

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        Task<bool> ExistsAsync(string key);

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
        /// 仅当Key不存在则设置其值
        /// </summary>
        bool SetNx(string key, object value);

        /// <summary>
        /// 仅当Key不存在则设置其值
        /// </summary>
        Task<bool> SetNxAsync(string key, object value);

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

        /// <summary>
        /// 计算给定字符串中被设置为1的比特位数量
        /// </summary>
        long BitCount(string key, long? start = null, long? end = null);

        /// <summary>
        /// 计算给定字符串中被设置为1的比特位数量
        /// </summary>
        Task<long> BitCountAsync(string key, long? start = null, long? end = null);

        /// <summary>
        /// 对Key所储存的字符串值设置或清除指定偏移量上的位
        /// </summary>
        bool SetBit(string key, uint offset, bool value);

        /// <summary>
        /// 对Key所储存的字符串值设置或清楚指定偏移量上的位
        /// </summary>
        Task<bool> SetBitAsync(string key, uint offset, bool value);

        /// <summary>
        /// 获取Key存储的字符串上指定偏移量的位
        /// </summary>
        bool GetBit(string key);

        /// <summary>
        /// 获取Key存储的字符串上指定偏移量的位
        /// </summary>
        Task<bool> GetBitAsync(string key);

        /// <summary>
        /// 将Key中储存的数字值减一
        /// </summary>
        long Decr(string key);

        /// <summary>
        /// 将Key中储存的数字值减一
        /// </summary>
        Task<long> DecrAsync(string key);

        /// <summary>
        /// 将Key中储存的数字减少固定值
        /// </summary>
        long DecrBy(string key, long decrement);

        /// <summary>
        /// 将Key中储存的数字减少固定值
        /// </summary>
        Task<long> DecrByAsync(string key, long decrement);

        /// <summary>
        /// 将Key中储存的数字增加一
        /// </summary>
        long Incr(string key);

        /// <summary>
        /// 将Key中储存的数字增加一
        /// </summary>
        Task<long> IncrAsync(string key);

        /// <summary>
        /// 将Key中储存的数字增加一
        /// </summary>
        long IncrBy(string key);

        /// <summary>
        /// 将Key中储存的数组增加一
        /// </summary>
        Task<long> IncrByAsync(string key);

        /// <summary>
        /// 删除哈希表Key中的一个或多个指定域
        /// </summary>
        long HDel(string key, params string[] fields);

        /// <summary>
        /// 删除哈希表Key中的一个或多个指定域
        /// </summary>
        Task<long> HDelAsync(string key, params string[] fields);

        /// <summary>
        /// 查看哈希表Key中给定域field是否存在
        /// </summary>
        bool HExists(string key, string field);

        /// <summary>
        /// 查看哈希表Key中给定域field是否存在
        /// </summary>
        Task<bool> HExistsAsync(string key, string field);

        /// <summary>
        /// 返回哈希表Key中指定域的值
        /// </summary>
        string HGet(string key, string field);

        /// <summary>
        /// 返回哈希表Key中指定域的值
        /// </summary>
        Task<string> HGetAsync(string key, string field);

        /// <summary>
        /// 返回哈希表Key中域的数量
        /// </summary>
        long HLen(string key);

        /// <summary>
        /// 返回哈希表Key中域的数量
        /// </summary>
        Task<long> HLenAsync(string key);

        /// <summary>
        /// 将哈希表Key中的域的值设置为value
        /// </summary>
        bool HSet(string key, string field, object value);

        /// <summary>
        /// 将哈希表Key中的域的值设置为value
        /// </summary>
        Task<bool> HSetAsync(string key, string field, object value);

        /// <summary>
        /// 将哈希Key中的域的值设置为Value，当且仅当域field不存在
        /// </summary>
        bool HSetNx(string key, string field, object value);

        /// <summary>
        /// 将哈希Key中的域的值设置为Value，当且仅当域field不存在
        /// </summary>
        Task<bool> HSetNxAsync(string key, string field, object value);

        /// <summary>
        /// 采用阻塞模式获取指定Key数组中任意值
        /// </summary>
        Tuple<string, string> BLPop(int timeout, params string[] keys);

        /// <summary>
        /// 采用阻塞模式获取指定key数组中任意值
        /// </summary>
        Task<Tuple<string, string>> BLPopAsync(int timeout, params string[] keys);

        /// <summary>
        /// 采用阻塞模式获取指定Key数组中任意值
        /// </summary>
        Tuple<string, string> BRPop(int timeout, params string[] keys);

        /// <summary>
        /// 采用阻塞模式获取指定Key数组中的任意值
        /// </summary>
        Task<Tuple<string, string>> BRPopAsync(int timeout, params string[] keys);

        /// <summary>
        /// 返回列表Key中指定下标的元素
        /// </summary>
        string LIndex(string key, long index);

        /// <summary>
        /// 返回列表Key中指定下标的元素
        /// </summary>
        Task<string> LIndexAsync(string key, long index);


    }
}
