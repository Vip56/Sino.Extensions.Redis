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

        public string Get(string key)
        {
            var cacheItem = _pool.Get(_instance + key);
            return cacheItem;
        }

        public async Task<string> GetAsync(string key)
        {
            var cacheItem = await _pool.GetAsync(_instance + key);
            return cacheItem;
        }

        public void Refresh(string key, int timeout)
        {
            _pool.Expire(_instance + key, timeout);
        }

        public async Task RefreshAsync(string key, int timeout)
        {
            await _pool.ExpireAsync(_instance + key, timeout);
        }

        public void Remove(string key) => _pool.Del(_instance + key);

        public async Task RemoveAsync(string key) => await _pool.DelAsync(_instance + key);

        public void Set(string key, string value, int? timeout = null) => _pool.Set(_instance + key, value, timeout);

        public async Task SetAsync(string key, string value, int? timeout = null) => await _pool.SetAsync(_instance + key, value, timeout);

        public void Dispose()
        {
            if (_pool != null)
            {
                _pool.Dispose();
            }
        }
    }
}
