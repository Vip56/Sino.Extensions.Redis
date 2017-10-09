using RedisUnitTest.Mock;
using Sino.Extensions.Redis;
using System.Net;
using Xunit;

namespace RedisUnitTest
{
    public class PubSubTests
    {
        [Fact]
        public void PublishTest()
        {
            using (var mock = new FakeRedisSocket(":3\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(3, redis.Publish("test", "message"));
                Assert.Equal("*3\r\n$7\r\nPUBLISH\r\n$4\r\ntest\r\n$7\r\nmessage\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void PubSubChannelsTest()
        {
            using (var mock = new FakeRedisSocket("*2\r\n$5\r\ntest1\r\n$5\r\ntest2\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                var response = redis.PubSubChannels("pattern");
                Assert.Equal(2, response.Length);
                Assert.Equal("test1", response[0]);
                Assert.Equal("test2", response[1]);
                Assert.Equal("*3\r\n$6\r\nPUBSUB\r\n$8\r\nCHANNELS\r\n$7\r\npattern\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void PubSubNumSubTest()
        {
            using (var mock = new FakeRedisSocket("*4\r\n$5\r\ntest1\r\n:1\r\n$5\r\ntest2\r\n:5\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                var response = redis.PubSubNumSub("channel1", "channel2");
                Assert.Equal(2, response.Length);
                Assert.Equal("test1", response[0].Item1);
                Assert.Equal(1, response[0].Item2);
                Assert.Equal("test2", response[1].Item1);
                Assert.Equal(5, response[1].Item2);
                Assert.Equal("*4\r\n$6\r\nPUBSUB\r\n$6\r\nNUMSUB\r\n$8\r\nchannel1\r\n$8\r\nchannel2\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void PubSubNumPatTest()
        {
            using (var mock = new FakeRedisSocket(":3\r\n"))
            using (var redis = new RedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(3, redis.PubSubNumPat());
                Assert.Equal("*2\r\n$6\r\nPUBSUB\r\n$6\r\nNUMPAT\r\n", mock.GetMessage());
            }
        }
    }
}
