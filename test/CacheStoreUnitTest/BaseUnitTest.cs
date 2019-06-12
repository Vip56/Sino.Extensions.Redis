using Newtonsoft.Json;
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

        public T Deserialize<T>(string obj, Encoding encoding = null)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }

        public Task<T> DeserializeAsync<T>(string obj, Encoding encoding = null)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<T>(obj));
        }

        public T DeserializeByte<T>(byte[] obj, Encoding encoding = null)
        {
            var str = Encoding.UTF8.GetString(obj);
            return JsonConvert.DeserializeObject<T>(str);
        }

        public Task<T> DeserializeByteAsync<T>(byte[] obj, Encoding encoding = null)
        {
            return Task.FromResult(DeserializeByte<T>(obj, encoding));
        }

        public string Serialize<T>(T obj, Encoding encoding = null)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public Task<string> SerializeAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(Serialize(obj));
        }

        public byte[] SerializeByte<T>(T obj, Encoding encoding = null)
        {
            var str = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(str);
        }

        public Task<byte[]> SerializeByteAsync<T>(T obj, Encoding encoding = null)
        {
            return Task.FromResult(SerializeByte(obj, encoding));
        }
    }
}
