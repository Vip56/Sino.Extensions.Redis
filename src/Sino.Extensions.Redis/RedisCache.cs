using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis
{
    /// <summary>
    /// IDistributedCache接口实现
    /// </summary>
    public class RedisCache : IDistributedCache, IDisposable
    {
        public TimeSpan Expiry { get; set; } = TimeSpan.FromDays(7);

        private readonly RedisCacheOptions _options;
        private readonly string _instance;
        private readonly PoolRedisClient _pool;

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

        public byte[] Get(string key)
        {
            try
            {
                var cacheItem = _pool.Get(_instance + key);
                if (!string.IsNullOrEmpty(cacheItem))
                    return Encoding.UTF8.GetBytes(cacheItem);
            }
            catch (Exception) { }
            return null;
        }

        public async Task<byte[]> GetAsync(string key)
        {
            try
            {
                var cacheItem = await _pool.GetAsync(_instance + key);
                if (!string.IsNullOrEmpty(cacheItem))
                    return Encoding.UTF8.GetBytes(cacheItem);
            }
            catch (Exception) { }
            return null;
        }

        public void Refresh(string key)
        {
            _pool.Expire(_instance + key, (int)Expiry.TotalSeconds);
        }

        public async Task RefreshAsync(string key)
        {
            await _pool.ExpireAsync(_instance + key, (int)Expiry.TotalSeconds);
        }

        public void Remove(string key)
        {
            _pool.Del(_instance + key);
        }

        public async Task RemoveAsync(string key)
        {
            await _pool.DelAsync(_instance + key);
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            string val = Encoding.UTF8.GetString(value);
            if (options.AbsoluteExpirationRelativeToNow != null)
            {
                _pool.Set(_instance + key, val, options.AbsoluteExpirationRelativeToNow.Value);
            }
            else
            {
                _pool.GetSet(_instance + key, val);
            }
        }

        public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            string val = Encoding.UTF8.GetString(value);
            if (options.AbsoluteExpirationRelativeToNow != null)
            {
                await _pool.SetAsync(_instance + key, val, options.AbsoluteExpirationRelativeToNow.Value);
            }
            else
            {
                await _pool.GetSetAsync(_instance + key, val);
            }
        }

        public void Dispose()
        {
            if (_pool != null)
            {
                _pool.Dispose();
            }
        }
    }
}
