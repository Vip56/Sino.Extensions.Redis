using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

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
            : this(optionsAccessor?.Value) { }

        public RedisCache(RedisCacheOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));

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

            return _pool.Exists(_instance + key);
        }

        public Task<bool> ExistsAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.ExistsAsync(_instance + key);
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

        public bool Expire(string key, int seconds)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.Expire(_instance + key, seconds);
        }

        public Task<bool> ExpireAsync(string key, int seconds)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.ExpireAsync(_instance + key, seconds);
        }

        public bool PExpire(string key, long milliseconds)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.PExpire(_instance + key, milliseconds);
        }

        public Task<bool> PExpireAsync(string key, long milliseconds)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.PExpireAsync(_instance + key, milliseconds);
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

            return _pool.SetNx(_instance + key, value);
        }

        public Task<bool> SetNxAsync(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.SetNxAsync(_instance + key, value);
        }

        public long Append(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.Append(_instance + key, value);
        }

        public Task<long> AppendAsync(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.AppendAsync(_instance + key, value);
        }

        public long StrLen(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.StrLen(_instance + key);
        }

        public Task<long> StrLenAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.StrLenAsync(_instance + key);
        }

        public string GetRange(string key, long start, long end)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.GetRange(_instance + key, start, end);
        }

        public Task<string> GetRangeAsync(string key, long start, long end)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.GetRangeAsync(_instance + key, start, end);
        }

        public long BitCount(string key, long? start = null, long? end = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.BitCount(_instance + key, start, end);
        }

        public Task<long> BitCountAsync(string key, long? start = null, long? end = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.BitCountAsync(_instance + key, start, end);
        }

        public bool SetBit(string key, uint offset, bool value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.SetBit(_instance + key, offset, value);
        }

        public Task<bool> SetBitAsync(string key, uint offset, bool value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.SetBitAsync(_instance + key, offset, value);
        }

        public bool GetBit(string key, uint offset)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.GetBit(_instance + key, offset);
        }

        public Task<bool> GetBitAsync(string key, uint offset)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.GetBitAsync(_instance + key, offset);
        }

        public long Decr(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.Decr(_instance + key);
        }

        public Task<long> DecrAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.DecrAsync(_instance + key);
        }

        public long DecrBy(string key, long decrement)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.DecrBy(_instance + key, decrement);
        }

        public Task<long> DecrByAsync(string key, long decrement)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.DecrByAsync(_instance + key, decrement);
        }

        public long Incr(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.Incr(_instance + key);
        }

        public Task<long> IncrAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.IncrAsync(_instance + key);
        }

        public long IncrBy(string key, long increment)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.IncrBy(_instance + key, increment);
        }

        public Task<long> IncrByAsync(string key, long increment)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.IncrByAsync(_instance + key, increment);
        }

        public long HDel(string key, params string[] fields)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));
            if (fields != null && fields.Length == 0)
                throw new ArgumentNullException(nameof(fields));

            return _pool.HDel(_instance + key, fields);
        }

        public Task<long> HDelAsync(string key, params string[] fields)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));
            if (fields != null && fields.Length == 0)
                throw new ArgumentNullException(nameof(fields));

            return _pool.HDelAsync(_instance + key, fields);
        }

        public bool HExists(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HExists(_instance + key, field);
        }

        public Task<bool> HExistsAsync(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HExistsAsync(_instance + key, field);
        }

        public string HGet(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HGet(_instance + key, field);
        }

        public Task<string> HGetAsync(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            return _pool.HGetAsync(_instance + key, field);
        }

        public long HLen(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.HLen(_instance + key);
        }

        public Task<long> HLenAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.HLenAsync(_instance + key);
        }

        public bool HSet(string key, string field, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.HSet(_instance + key, field, value);
        }

        public Task<bool> HSetAsync(string key, string field, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.HSetAsync(_instance + key, field, value);
        }

        public bool HSetNx(string key, string field, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.HSetNx(_instance + key, field, value);
        }

        public Task<bool> HSetNxAsync(string key, string field, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.HSetNxAsync(_instance + key, field, value);
        }

        public Tuple<string, string> BLPop(int timeout, params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keys != null && keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            return _pool.BLPop(timeout, keys.AddPrefix(_instance).ToArray());
        }

        public Task<Tuple<string, string>> BLPopAsync(int timeout, params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keys != null && keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            return _pool.BLPopAsync(timeout, keys.AddPrefix(_instance).ToArray());
        }

        public Tuple<string, string> BRPop(int timeout, params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keys != null && keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            return _pool.BRPop(timeout, keys.AddPrefix(_instance).ToArray());
        }

        public Task<Tuple<string, string>> BRPopAsync(int timeout, params string[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keys != null && keys.Length == 0)
                throw new ArgumentNullException(nameof(keys));

            return _pool.BRPopAsync(timeout, keys.AddPrefix(_instance).ToArray());
        }

        public string LIndex(string key, long index)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.LIndex(_instance + key, index);
        }

        public Task<string> LIndexAsync(string key, long index)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.LIndexAsync(_instance + key, index);
        }

        public long LLen(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.LLen(_instance + key);
        }

        public Task<long> LLenAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.LLenAsync(_instance + key);
        }

        public long LPush(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.LPush(_instance + key, values);
        }

        public Task<long> LPushAsync(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.LPushAsync(_instance + key, values);
        }

        public long LPushX(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.LPushX(_instance + key, value);
        }

        public Task<long> LPushXAsync(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.LPushXAsync(_instance + key, value);
        }

        public string RPop(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.RPop(_instance + key);
        }

        public Task<string> RPopAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            return _pool.RPopAsync(_instance + key);
        }

        public long RPush(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.RPush(_instance + key, values);
        }

        public Task<long> RPushAsync(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.RPushAsync(_instance + key, values);
        }

        public long RPushX(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.RPushX(_instance + key, values);
        }

        public Task<long> RPushXAsync(string key, params string[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values != null && values.Length == 0)
                throw new ArgumentNullException(nameof(values));

            return _pool.RPushXAsync(_instance + key, values);
        }

        public string BRPopLPush(string source, string destination, int timeout)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            return _pool.BRPopLPush(_instance + source, _instance + destination, timeout);
        }

        public Task<string> BRPopLPushAsync(string source, string destination, int timeout)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            return _pool.BRPopLPushAsync(_instance + source, _instance + destination, timeout);
        }

        public string RPopLPush(string source, string destination)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            return _pool.RPopLPush(_instance + source, _instance + destination);
        }

        public Task<string> RPopLPushAsync(string source, string destination)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException(nameof(source));
            if (string.IsNullOrEmpty(destination))
                throw new ArgumentNullException(nameof(destination));

            return _pool.RPopLPushAsync(_instance + source, _instance + destination);
        }

        public long LRem(string key, long count, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.LRem(_instance + key, count, value);
        }

        public Task<long> LRemAsync(string key, long count, string value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            return _pool.LRemAsync(_instance + key, count, value);
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
