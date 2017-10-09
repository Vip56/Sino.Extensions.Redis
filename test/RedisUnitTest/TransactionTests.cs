using RedisUnitTest.Mock;
using Sino.Extensions.Redis;
using System.Net;
using Xunit;

namespace RedisUnitTest
{
    public class TransactionTests
    {
        [Fact]
        public void DiscardTest()
        {
            using (var mock = new FakeRedisSocket("+OK\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("OK", redis.Discard());
                Assert.Equal("*1\r\n$7\r\nDISCARD\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void ExecTest()
        {
            using (var mock = new FakeRedisSocket("*1\r\n$2\r\nhi\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                var response = redis.Exec();
                Assert.Equal(1, response.Length);
                Assert.Equal("hi", response[0]);
                Assert.Equal("*1\r\n$4\r\nEXEC\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void MultiTest()
        {
            using (var mock = new FakeRedisSocket("+OK\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("OK", redis.Multi());
                Assert.Equal("*1\r\n$5\r\nMULTI\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void UnwatchTest()
        {
            using (var mock = new FakeRedisSocket("+OK\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("OK", redis.Unwatch());
                Assert.Equal("*1\r\n$7\r\nUNWATCH\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void WatchTest()
        {
            using (var mock = new FakeRedisSocket("+OK\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("OK", redis.Watch());
                Assert.Equal("*1\r\n$5\r\nWATCH\r\n", mock.GetMessage());
            }
        }
    }
}
