﻿using Sino.CacheStore.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore
{
    /// <summary>
    /// 缓存存储接口
    /// </summary>
    public interface ICacheStore : ICacheStoreEvent
    {
        Task Init();

        #region Basic

        /// <summary>
        /// 心跳检测
        /// </summary>
        string Ping();

        Task<string> PingAsync();

        /// <summary>
        /// 退出
        /// </summary>
        string Quit();

        Task<string> QuitAsync();

        /// <summary>
        /// 选择数据库
        /// </summary>
        string Select(string dbName);

        Task<string> SelectAsync(string dbName);

        #endregion

        #region Key

        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key">需要判断的Key</param>
        /// <returns>True代表存在，反之不存在</returns>
        bool Exists(string key);

        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// 根据Key返回缓存项
        /// </summary>
        /// <param name="key">需要检索的Key</param>
        /// <returns>保存的值</returns>
        /// <remarks>不存在则返回Null</remarks>
        T Get<T>(string key) where T : class;

        Task<T> GetAsync<T>(string key) where T : class;

        /// <summary>
        /// 根据Key返回缓存项
        /// </summary>
        /// <param name="key">需要检索的Key</param>
        /// <returns>保存的值</returns>
        byte[] GetBytes(string key);

        Task<byte[]> GetBytesAsync(string key);

        /// <summary>
        /// 设置Key的值
        /// </summary>
        /// <param name="key">需要保存的Key</param>
        /// <param name="value">需要保存的值</param>
        /// <param name="seconds">超时时间，单位秒</param>
        /// <param name="milliSeconds">超时时间，单位毫秒</param>
        string Set<T>(string key, T value, int? seconds = null, long? milliSeconds = null) where T : class;

        Task<string> SetAsync<T>(string key, T value, int? seconds = null, long? milliSeconds = null) where T : class;

        /// <summary>
        /// 设置Key的值
        /// </summary>
        /// <param name="key">需要保存得Key</param>
        /// <param name="value">需要保存得值</param>
        /// <param name="seconds">超时时间，单位秒</param>
        /// <param name="milliSeconds">超时时间，单位毫秒</param>
        string SetBytes(string key, byte[] value, int? seconds = null, long? milliSeconds = null);

        Task<string> SetBytesAsync(string key, byte[] value, int? seconds = null, long? milliSeconds = null);

        /// <summary>
        /// 仅当Key不存在时设置其值
        /// </summary>
        /// <param name="key">需要保存的Key</param>
        /// <param name="value">需要保存的值</param>
        /// <param name="seconds">超时时间，单位秒</param>
        /// <param name="milliSeconds">超时时间，单位毫秒</param>
        /// <returns>是否成功设置</returns>
        string SetWithNoExisted<T>(string key, T value, int? seconds = null, long? milliSeconds = null) where T : class;

        Task<string> SetWithNoExistedAsync<T>(string key, T value, int? seconds = null, long? milliSeconds = null) where T : class;

        /// <summary>
        /// 仅当Key不存在时设置其值
        /// </summary>
        /// <param name="key">需要保存的Key</param>
        /// <param name="value">需要保存的值</param>
        /// <param name="seconds">超时时间，单位秒</param>
        /// <param name="milliSeconds">超时时间，单位毫秒</param>
        /// <returns>是否成功设置</returns>
        string SetWithNoExistedBytes(string key, byte[] value, int? seconds = null, long? milliSeconds = null);

        Task<string> SetWithNoExistedBytesAsync(string key, byte[] value, int? seconds = null, long? milliSeconds = null);

        /// <summary>
        /// 设置Key的超时时间
        /// </summary>
        /// <param name="key">需要设置的Key</param>
        /// <param name="value">超时时间,默认为秒</param>
        /// <param name="isSeconds">超时时间单位，true为秒，反之为毫秒</param>
        bool Expire(string key, long value, bool isSeconds = true);

        Task<bool> ExpireAsync(string key, long value, bool isSeconds = true);

        /// <summary>
        /// 移除Key
        /// </summary>
        /// <param name="key">需要移除的Key</param>
        long Remove(string key);

        Task<long> RemoveAsync(string key);

        #endregion

        #region BitAndNumber

        /// <summary>
        /// 计算给定Key中被设置为1的比特位数量
        /// </summary>
        /// <param name="key">需要获取的Key</param>
        /// <param name="start">起始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns>存在的数量</returns>
        long BitCount(string key, long? start = null, long? end = null);

        Task<long> BitCountAsync(string key, long? start = null, long? end = null);

        /// <summary>
        /// 对Key所存储的值设置或清除指定偏移量上的位
        /// </summary>
        /// <param name="key">需要设置的Key</param>
        /// <param name="offset">偏移量</param>
        /// <param name="value">需要设置的值</param>
        /// <returns>设置的值</returns>
        bool SetBit(string key, uint offset, bool value);

        Task<bool> SetBitAsync(string key, uint offset, bool value);

        /// <summary>
        /// 获取Key上指定位置存储的值
        /// </summary>
        /// <param name="key">需要获取的Key</param>
        /// <param name="offset">偏移量</param>
        /// <returns>存储的值</returns>
        bool GetBit(string key, uint offset);

        Task<bool> GetBitAsync(string key, uint offset);

        /// <summary>
        /// 对Key中存储的值递减
        /// </summary>
        /// <param name="key">需要设置的Key</param>
        /// <returns>递减后的值</returns>
        long Decr(string key);

        Task<long> DecrAsync(string key);

        /// <summary>
        /// 对Key中存储的值减指定值
        /// </summary>
        /// <param name="key">需要设置的Key</param>
        /// <param name="decrement">需要减的值</param>
        /// <returns>减后的值</returns>
        long DecrBy(string key, long decrement);

        Task<long> DecrByAsync(string key, long decrement);

        /// <summary>
        /// 对Key存储的值递增
        /// </summary>
        /// <param name="key">需要递增的Key</param>
        /// <returns>递增后的值</returns>
        long Incr(string key);

        Task<long> IncrAsync(string key);

        /// <summary>
        /// 对Key中存的值加指定值
        /// </summary>
        /// <param name="key">需要设置的Key</param>
        /// <param name="increment">需要加的值</param>
        /// <returns>加后的值</returns>
        long IncrBy(string key, long increment);

        Task<long> IncrByAsync(string key, long increment);

        #endregion

        #region Hash

        /// <summary>
        /// 删除哈希表中一个或多个域
        /// </summary>
        /// <param name="key">需要删除的Key</param>
        /// <param name="fields">需要删除的域</param>
        /// <returns>删除个数</returns>
        long HDel(string key, params string[] fields);

        Task<long> HDelAsync(string key, params string[] fields);

        /// <summary>
        /// 查看哈希表中指定域是否存在
        /// </summary>
        /// <param name="key">需要查看的Key</param>
        /// <param name="field">需要查看的域</param>
        /// <returns>是否存在</returns>
        bool HExists(string key, string field);

        Task<bool> HExistsAsync(string key, string field);

        /// <summary>
        /// 获取哈希表中指定域的值
        /// </summary>
        /// <param name="key">需要获取的Key</param>
        /// <param name="field">需要获取的域</param>
        /// <returns>保存的值，不存在则返回Null</returns>
        T HGet<T>(string key, string field) where T : class;

        Task<T> HGetAsync<T>(string key, string field) where T : class;

        /// <summary>
        /// 获取哈希表中指定域的值
        /// </summary>
        /// <param name="key">需要获取的Key</param>
        /// <param name="field">需要获取的域</param>
        /// <returns>保存的值，不存在则返回Null</returns>
        byte[] HGetBytes(string key, string field);

        Task<byte[]> HGetBytesAsync(string key, string field);

        /// <summary>
        /// 查看哈希表中域的数量
        /// </summary>
        /// <param name="key">需要查看的Key</param>
        /// <returns>域的数量</returns>
        long HLen(string key);

        Task<long> HLenAsync(string key);

        /// <summary>
        /// 设置哈希表中指定域的值
        /// </summary>
        /// <param name="key">需要设置的Key</param>
        /// <param name="field">需要设置的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>是否保存成功</returns>
        bool HSet<T>(string key, string field, T value) where T : class;

        Task<bool> HSetAsync<T>(string key, string field, T value) where T : class;

        /// <summary>
        /// 设置哈希表中指定域的值
        /// </summary>
        /// <param name="key">需要设置的Key</param>
        /// <param name="field">需要设置的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>是否保存成功</returns>
        bool HSetBytes(string key, string field, byte[] value);

        Task<bool> HSetBytesAsync(string key, string field, byte[] value);

        /// <summary>
        /// 仅当不存在时设置哈希表中指定域的值
        /// </summary>
        /// <param name="key">需要设置的Key</param>
        /// <param name="field">需要设置的域</param>
        /// <param name="value">需要保存的值</param>
        /// <returns>是否保存成功</returns>
        bool HSetWithNoExisted<T>(string key, string field, T value) where T : class;

        Task<bool> HSetWithNoExistedAsync<T>(string key, string field, T value) where T : class;

        bool HSetWithNoExistedBytes(string key, string field, byte[] value);

        Task<bool> HSetWithNoExistedBytesAsync(string key, string field, byte[] value);

        #endregion

        #region List

        /// <summary>
        /// 移除并返回列表Key的头元素
        /// </summary>
        /// <param name="key">需要操作的列表Key</param>
        /// <returns>头元素</returns>
        T LPop<T>(string key) where T : class;

        Task<T> LPopAsync<T>(string key) where T : class;

        /// <summary>
        /// 移除并返回列表Key的头元素
        /// </summary>
        /// <param name="key">需要操作的列表Key</param>
        /// <returns>头元素</returns>
        byte[] LPopBytes(string key);

        Task<byte[]> LPopBytesAsync(string key);

        /// <summary>
        /// 获取列表中指定下标的元素
        /// </summary>
        /// <param name="key">需要获取的列表</param>
        /// <param name="index">元素的下标</param>
        /// <returns>获取的值</returns>
        T LIndex<T>(string key, long index) where T : class;

        Task<T> LIndexAsync<T>(string key, long index) where T : class;

        /// <summary>
        /// 获取列表中指定下标的元素
        /// </summary>
        /// <param name="key">需要获取的列表</param>
        /// <param name="index">元素的下标</param>
        /// <returns>获取的值</returns>
        byte[] LIndexBytes(string key, long index);

        Task<byte[]> LIndexBytesAsync(string key, long index);

        /// <summary>
        /// 获取列表的长度
        /// </summary>
        /// <param name="key">需要查询的列表</param>
        /// <returns>列表长度</returns>
        long LLen(string key);

        Task<long> LLenAsync(string key);

        /// <summary>
        /// 将值插入列表的头部
        /// </summary>
        /// <param name="key">需要插入的列表</param>
        /// <param name="value">插入的值</param>
        /// <returns>成功插入的值数量</returns>
        long LPush<T>(string key, T value) where T : class;

        Task<long> LPushAsync<T>(string key, T value) where T : class;

        /// <summary>
        /// 将值插入列表的头部
        /// </summary>
        /// <param name="key">需要插入的列表</param>
        /// <param name="value">插入的值</param>
        /// <returns>成功插入的值数量</returns>
        long LPushBytes(string key, byte[] value);

        Task<long> LPushBytesAsync(string key, byte[] value);

        /// <summary>
        /// 移除并返回列表的尾元素
        /// </summary>
        /// <param name="key">需要操作的列表</param>
        /// <returns>元素值</returns>
        T RPop<T>(string key) where T : class;

        Task<T> RPopAsync<T>(string key) where T : class;

        /// <summary>
        /// 移除并返回列表的尾元素
        /// </summary>
        /// <param name="key">需要操作的列表</param>
        /// <returns>元素值</returns>
        byte[] RPopBytes(string key);

        Task<byte[]> RPopBytesAsync(string key);

        /// <summary>
        /// 将一个或多个值插入列表尾部
        /// </summary>
        /// <param name="key">需要插入的列表</param>
        /// <param name="values">插入的值</param>
        /// <returns>成功插入的值数量</returns>
        long RPush<T>(string key, T values) where T : class;

        Task<long> RPushAsync<T>(string key, T values) where T : class;

        /// <summary>
        /// 将一个或多个值插入列表尾部
        /// </summary>
        /// <param name="key">需要插入的列表</param>
        /// <param name="value">插入的值</param>
        /// <returns>成功插入的值数量</returns>
        long RPushBytes(string key, byte[] value);

        Task<long> RPushBytesAsync(string key, byte[] value);

        #endregion
    }
}
