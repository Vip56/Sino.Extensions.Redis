using System;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis.Internal.IO
{
    interface IRedisAsyncCommandToken
    {
        Task Task { get; }

        RedisCommand Command { get; }

        void SetResult(RedisReader reader);

        void SetException(Exception e);
    }
}
