using RedisUnitTest.Mock;
using Sino.Extensions.Redis;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace RedisUnitTest
{
    public class MyGeneric
    {
        public string field1 { get; set; }
    }

    public class HashTests
    {
        [Fact]
        public void TestHDel()
        {
            using (var mock = new FakeRedisSocket(":2\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(2, redis.HDel("test", "test1", "test2"));
                Assert.Equal("*4\r\n$4\r\nHDEL\r\n$4\r\ntest\r\n$5\r\ntest1\r\n$5\r\ntest2\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHExists()
        {
            using (var mock = new FakeRedisSocket(":1\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.HExists("test", "field"));
                Assert.Equal("*3\r\n$7\r\nHEXISTS\r\n$4\r\ntest\r\n$5\r\nfield\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHGet()
        {
            using (var mock = new FakeRedisSocket("$4\r\ntest\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("test", redis.HGet("test", "field"));
                Assert.Equal("*3\r\n$4\r\nHGET\r\n$4\r\ntest\r\n$5\r\nfield\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHGetAll_Dictionary()
        {
            using (var mock = new FakeRedisSocket("*2\r\n$6\r\nfield1\r\n$5\r\ntest1\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                var response = redis.HGetAll("test");
                Assert.Equal(1, response.Count);
                Assert.True(response.ContainsKey("field1"));
                Assert.Equal("test1", response["field1"]);
                Assert.Equal("*2\r\n$7\r\nHGETALL\r\n$4\r\ntest\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHIncrBy()
        {
            using (var mock = new FakeRedisSocket(":5\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(5, redis.HIncrBy("test", "field", 1));
                Assert.Equal("*4\r\n$7\r\nHINCRBY\r\n$4\r\ntest\r\n$5\r\nfield\r\n$1\r\n1\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHIncrByFloat()
        {
            using (var mock = new FakeRedisSocket("$4\r\n3.14\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(3.14, redis.HIncrByFloat("test", "field", 1.14));
                Assert.Equal("*4\r\n$12\r\nHINCRBYFLOAT\r\n$4\r\ntest\r\n$5\r\nfield\r\n$4\r\n1.14\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHKeys()
        {
            using (var mock = new FakeRedisSocket("*2\r\n$5\r\ntest1\r\n$5\r\ntest2\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                var response = redis.HKeys("test");
                Assert.Equal(2, response.Length);
                Assert.Equal("test1", response[0]);
                Assert.Equal("test2", response[1]);
                Assert.Equal("*2\r\n$5\r\nHKEYS\r\n$4\r\ntest\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHLen()
        {
            using (var mock = new FakeRedisSocket(":5\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(5, redis.HLen("test"));
                Assert.Equal("*2\r\n$4\r\nHLEN\r\n$4\r\ntest\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHMGet()
        {
            using (var mock = new FakeRedisSocket("*2\r\n$5\r\ntest1\r\n$5\r\ntest2\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                var response = redis.HMGet("test", "field1", "field2");
                Assert.Equal(2, response.Length);
                Assert.Equal("test1", response[0]);
                Assert.Equal("test2", response[1]);
                Assert.Equal("*4\r\n$5\r\nHMGET\r\n$4\r\ntest\r\n$6\r\nfield1\r\n$6\r\nfield2\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHMSet_Dictionary()
        {
            using (var mock = new FakeRedisSocket("+OK\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("OK", redis.HMSet("test", new Dictionary<string, string> { { "field1", "test1" } }));
                Assert.Equal("*4\r\n$5\r\nHMSET\r\n$4\r\ntest\r\n$6\r\nfield1\r\n$5\r\ntest1\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHSet()
        {
            using (var mock = new FakeRedisSocket(":1\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.HSet("test", "field1", "test1"));
                Assert.Equal("*4\r\n$4\r\nHSET\r\n$4\r\ntest\r\n$6\r\nfield1\r\n$5\r\ntest1\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestHSetNX()
        {
            using (var mock = new FakeRedisSocket(":1\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.HSetNx("test", "field1", "test1"));
                Assert.Equal("*4\r\n$6\r\nHSETNX\r\n$4\r\ntest\r\n$6\r\nfield1\r\n$5\r\ntest1\r\n", mock.GetMessage());
            }
        }
    }
}
