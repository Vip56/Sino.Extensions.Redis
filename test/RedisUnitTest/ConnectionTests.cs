using RedisUnitTest.Mock;
using Sino.Extensions.Redis;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace RedisUnitTest
{
    public class ConnectionTests
    {
        class TestHelper
        {
            public const string Host = "fakehost";
            public const int Port = 9999;

            public static void Test<T>(string reply, Func<PoolRedisClient, T> syncFunc, Func<PoolRedisClient, Task<T>> asyncFunc, Action<FakeRedisSocket, T> test)
            {
                using (var mock = new FakeRedisSocket(reply, reply))
                using (var redis = new PoolRedisClient(mock, new DnsEndPoint(Host, Port)))
                {
                    if (syncFunc != null)
                    {
                        var r1 = syncFunc(redis);
                        test(mock, r1);
                    }

                    if (asyncFunc != null)
                    {
                        var r2 = asyncFunc(redis);
                        test(mock, r2.Result);
                    }
                }
            }
        }

        [Fact]
        public void EchoTest()
        {
            TestHelper.Test(
                "$11\r\nhello world\r\n",
                x => x.Echo("hello world"),
                x => x.EchoAsync("hello world"),
                (x, r) =>
                {
                    Assert.Equal("hello world", r);
                    Assert.Equal("*2\r\n$4\r\nECHO\r\n$11\r\nhello world\r\n", x.GetMessage());
                });
        }

        [Fact]
        public void PingTest()
        {
            TestHelper.Test(
                "+PONG\r\n",
                x => x.Ping(),
                x => x.PingAsync(),
                (x, r) =>
                {
                    Assert.Equal("PONG", r);
                    Assert.Equal("*1\r\n$4\r\nPING\r\n", x.GetMessage());
                });
        }

        [Fact]
        public void QuitTest()
        {
            TestHelper.Test(
                "+OK\r\n",
                x => x.Quit(),
                null,
                (x, r) =>
                {
                    Assert.Equal("OK", r);
                    Assert.Equal("*1\r\n$4\r\nQUIT\r\n", x.GetMessage());
                });

            TestHelper.Test(
                "+OK\r\n",
                null,
                x => x.QuitAsync(),
                (x, r) =>
                {
                    Assert.Equal("OK", r);
                    Assert.Equal("*1\r\n$4\r\nQUIT\r\n", x.GetMessage());
                });
        }

        [Fact]
        public void SelectTest()
        {
            TestHelper.Test(
                "+OK\r\n",
                x => x.Select(2),
                x => x.SelectAsync(2),
                (x, r) =>
                {
                    Assert.Equal("OK", r);
                    Assert.Equal("*2\r\n$6\r\nSELECT\r\n$1\r\n2\r\n", x.GetMessage());
                });
        }
    }
}
