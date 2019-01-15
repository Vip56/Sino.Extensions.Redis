using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis
{
    /// <summary>
    /// IRedisCache接口实现
    /// </summary>
    public class RedisCache : IRedisCache, IDisposable
    {
        private readonly RedisCacheOptions _options;
        private readonly string _instance;
        private readonly PoolRedisClient _pool;

        public PoolRedisClient Client { get { return _pool; } }

        public RedisCache(IOptions<RedisCacheOptions> optionsAccessor)
        {
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException(nameof(optionsAccessor));
            }

            _options = optionsAccessor.Value;

            _instance = _options.InstanceName ?? string.Empty;

            if (string.IsNullOrEmpty(_options.Host))
            {
                throw new ArgumentNullException(nameof(_options.Host));
            }

            if (string.IsNullOrEmpty(_options.Password))
            {
                _pool = new PoolRedisClient(_options.Host, _options.Port);
            }
            else
            {
                _pool = new PoolRedisClient(_options.Host, _options.Port, _options.Password);
            }
        }

        #region IRedisCache

        public string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.Get(_instance + key);
        }

        public Task<string> GetAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.GetAsync(_instance + key);
        }

        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.Exists(key);
        }

        public Task<bool> ExistsAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.ExistsAsync(key);
        }

        public void Refresh(string key, int timeout)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (timeout < 0)
                throw new ArgumentOutOfRangeException(nameof(timeout));

            _pool.Expire(_instance + key, timeout);
        }

        public Task RefreshAsync(string key, int timeout)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (timeout < 0)
                throw new ArgumentOutOfRangeException(nameof(timeout));

            return _pool.ExpireAsync(_instance + key, timeout);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            _pool.Del(_instance + key);
        }

        public Task RemoveAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.DelAsync(_instance + key);
        }

        public void Set(string key, string value, int? timeout = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            _pool.Set(_instance + key, value, timeout);
        }

        public Task SetAsync(string key, string value, int? timeout = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(key));

            return _pool.SetAsync(_instance + key, value, timeout);
        }

        public bool SetNx(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.SetNx(key, value);
        }

        public Task<bool> SetNxAsync(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.SetNxAsync(key, value);
        }

        public long Append(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.Append(key, value);
        }

        public Task<long> AppendAsync(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.AppendAsync(key, value);
        }

        public long StrLen(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.StrLen(key);
        }

        public Task<long> StrLenAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.StrLenAsync(key);
        }

        public string GetRange(string key, long start, long end)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (end < 0)
                throw new ArgumentOutOfRangeException(nameof(end));

            return _pool.GetRange(key, start, end);
        }

        public Task<string> GetRangeAsync(string key, long start, long end)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));
            if (end < 0)
                throw new ArgumentOutOfRangeException(nameof(end));

            return _pool.GetRangeAsync(key, start, end);
        }

        public long BitCount(string key, long? start = null, long? end = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.BitCount(key, start, end);
        }

        public Task<long> BitCountAsync(string key, long? start = null, long? end = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.BitCountAsync(key, start, end);
        }

        public bool SetBit(string key, uint offset, bool value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.SetBit(key, offset, value);
        }

        public Task<bool> SetBitAsync(string key, uint offset, bool value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.SetBitAsync(key, offset, value);
        }

        public bool GetBit(string key, uint offset)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.GetBit(key, offset);
        }

        public Task<bool> GetBitAsync(string key, uint offset)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.GetBitAsync(key, offset);
        }

        public long Decr(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.Decr(key);
        }

        public Task<long> DecrAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.DecrAsync(key);
        }

        public long DecrBy(string key, long decrement)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.DecrBy(key, decrement);
        }

        public Task<long> DecrByAsync(string key, long decrement)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.DecrByAsync(key, decrement);
        }

        public long Incr(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.Incr(key);
        }

        public Task<long> IncrAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.IncrAsync(key);
        }

        public long IncrBy(string key, long increment)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.IncrBy(key, increment);
        }

        public Task<long> IncrByAsync(string key, long increment)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.IncrByAsync(key, increment);
        }

        public long HDel(string key, params string[] fields)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (fields != null && fields.Length > 0)
                throw new ArgumentNullException(nameof(fields));

            return _pool.HDel(key, fields);
        }

        public Task<long> HDelAsync(string key, params string[] fields)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (fields != null && fields.Length > 0)
                throw new ArgumentNullException(nameof(fields));

            return _pool.HDelAsync(key, fields);
        }

        public bool HExists(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HExists(key, field);
        }

        public Task<bool> HExistsAsync(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HExistsAsync(key, field);
        }

        public string HGet(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HGet(key, field);
        }

        public Task<string> HGetAsync(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HGetAsync(key, field);
        }

        public long HLen(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.HLen(key);
        }

        public Task<long> HLenAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.HLenAsync(key);
        }

        public bool HSet(string key, string field, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HSet(key, field, value);
        }

        public Task<bool> HSetAsync(string key, string field, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HSetAsync(key, field, value);
        }

        public bool HSetNx(string key, string field, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HSetNx(key, field, value);
        }

        public Task<bool> HSetNxAsync(string key, string field, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HSetNxAsync(key, field, value);
        }

        public Tuple<string, string> BLPop(int timeout, params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keys != null && keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            return _pool.BLPop(timeout, keys);
        }

        public Task<Tuple<string, string>> BLPopAsync(int timeout, params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keys != null && keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            return _pool.BLPopAsync(timeout, keys);
        }

        public Tuple<string, string> BRPop(int timeout, params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keys != null && keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            return _pool.BRPop(timeout, keys);
        }

        public Task<Tuple<string, string>> BRPopAsync(int timeout, params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keys != null && keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            return _pool.BRPopAsync(timeout, keys);
        }

        public string LIndex(string key, long index)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.LIndex(key, index);
        }

        public Task<string> LIndexAsync(string key, long index)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.LIndexAsync(key, index);
        }

        public long LLen(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.LLen(key);
        }

        public Task<long> LLenAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.LLenAsync(key);
        }

        public long LPush(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.LPush(key, values);
        }

        public Task<long> LPushAsync(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.LPushAsync(key, values);
        }

        public long LPushX(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.LPushX(key, value);
        }

        public Task<long> LPushXAsync(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.LPushXAsync(key, value);
        }

        public string RPop(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.RPop(key);
        }

        public Task<string> RPopAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.RPopAsync(key);
        }

        public long RPush(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.RPush(key, values);
        }

        public Task<long> RPushAsync(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.RPushAsync(key, values);
        }

        public long RPushX(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.RPushX(key, values);
        }

        public Task<long> RPushXAsync(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.RPushXAsync(key, values);
        }

        public string BRPopLPush(string source, string destination, int timeout)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            return _pool.BRPopLPush(source, destination, timeout);
        }

        public Task<string> BRPopLPushAsync(string source, string destination, int timeout)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            return _pool.BRPopLPushAsync(source, destination, timeout);
        }

        public string RPopLPush(string source, string destination)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            return _pool.RPopLPush(source, destination);
        }

        public Task<string> RPopLPushAsync(string source, string destination)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            return _pool.RPopLPushAsync(source, destination);
        }

        public long LRem(string key, long count, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.LRem(key, count, value);
        }

        public Task<long> LRemAsync(string key, long count, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.LRemAsync(key, count, value);
        }

        #endregion

        public void Dispose()
        {
            if (_pool != null)
            {
                _pool.Dispose();
            }
        }
    }
}
