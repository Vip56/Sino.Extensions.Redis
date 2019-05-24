using Sino.CacheStore;
using Sino.CacheStore.Configuration;
using Sino.CacheStore.Handler;
using Sino.CacheStore.Internal;
using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CacheStoreUnitTest
{
    public class BaseUnitTest : IConvertProvider
    {
        protected async Task Test<T>(string reply, Func<ICacheStore, T> syncFunc,
            Func<ICacheStore, Task<T>> asyncFunc, Action<FakeCacheStorePipeline, T> test)
        {
            var cmdFactory = new RedisCommandFactory();
            var pipeline = new FakeCacheStorePipeline(reply);
            var handler = new RedisCacheStoreHandler(new CacheStoreOptions(), pipeline);
            var client = new CacheStoreClient(cmdFactory, handler, this);

            if (syncFunc != null)
            {
                var r1 = syncFunc(client);
                test(pipeline, r1);
            }

            if (asyncFunc != null)
            {
                var r2 = await asyncFunc(client);
                test(pipeline, r2);
            }
        }

        public T Deserialize<T>(string obj, Encoding encoding = null) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null) where T : class
        {
            throw new NotImplementedException();
        }

        public T DeserializeByte<T>(byte[] obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null) where T : class
        {
            throw new NotImplementedException();
        }

        public string Serialize<T>(T obj, Encoding encoding = null) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<string> SerializeAsync<T>(T obj, Encoding encoding = null) where T : class
        {
            throw new NotImplementedException();
        }

        public byte[] SerializeByte<T>(T obj, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
