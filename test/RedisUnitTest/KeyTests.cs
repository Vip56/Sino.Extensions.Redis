using RedisUnitTest.Mock;
using Sino.Extensions.Redis;
using System;
using System.Net;
using System.Text;
using Xunit;

namespace RedisUnitTest
{
    public class KeyTests
    {
        [Fact]
        public void TestDel()
        {
            using (var mock = new FakeRedisSocket(":3\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(3, redis.Del("test"));
                Assert.Equal("*2\r\n$3\r\nDEL\r\n$4\r\ntest\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestDump()
        {
            using (var mock = new FakeRedisSocket("$4\r\ntest\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("test", Encoding.ASCII.GetString(redis.Dump("test")));
                Assert.Equal("*2\r\n$4\r\nDUMP\r\n$4\r\ntest\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestExists()
        {
            using (var mock = new FakeRedisSocket(":1\r\n", ":0\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.Exists("test1"));
                Assert.Equal("*2\r\n$6\r\nEXISTS\r\n$5\r\ntest1\r\n", mock.GetMessage());
                Assert.False(redis.Exists("test2"));
                Assert.Equal("*2\r\n$6\r\nEXISTS\r\n$5\r\ntest2\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestExpire()
        {
            using (var mock = new FakeRedisSocket(":1\r\n", ":0\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.Expire("test1", (int)TimeSpan.FromSeconds(10).TotalSeconds));
                Assert.Equal("*3\r\n$6\r\nEXPIRE\r\n$5\r\ntest1\r\n$2\r\n10\r\n", mock.GetMessage());
                Assert.False(redis.Expire("test2", 20));
                Assert.Equal("*3\r\n$6\r\nEXPIRE\r\n$5\r\ntest2\r\n$2\r\n20\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestExpireAt()
        {
            using (var mock = new FakeRedisSocket(":1\r\n", ":0\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.ExpireAt("test1", new DateTime(2014, 6, 13, 7, 6, 48)));
                Assert.Equal("*3\r\n$8\r\nEXPIREAT\r\n$5\r\ntest1\r\n$10\r\n1402614408\r\n", mock.GetMessage());
                Assert.False(redis.ExpireAt("test2", 1402643208));
                Assert.Equal("*3\r\n$8\r\nEXPIREAT\r\n$5\r\ntest2\r\n$10\r\n1402643208\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestKeys()
        {
            using (var mock = new FakeRedisSocket("*3\r\n$5\r\ntest1\r\n$5\r\ntest2\r\n$5\r\ntest3\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                var response = redis.Keys("test*");
                Assert.Equal(3, response.Length);
                for (int i = 0; i < response.Length; i++)
                    Assert.Equal("test" + (i + 1), response[i]);
            }
        }

        [Fact]
        public void TestMigrate()
        {
            using (var mock = new FakeRedisSocket("+OK\r\n", "+OK\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("OK", redis.Migrate("myhost", 1234, "mykey", 3, 1000));
                Assert.Equal("*6\r\n$7\r\nMIGRATE\r\n$6\r\nmyhost\r\n$4\r\n1234\r\n$5\r\nmykey\r\n$1\r\n3\r\n$4\r\n1000\r\n", mock.GetMessage());

                Assert.Equal("OK", redis.Migrate("myhost2", 1235, "mykey2", 6, TimeSpan.FromMilliseconds(100)));
                Assert.Equal("*6\r\n$7\r\nMIGRATE\r\n$7\r\nmyhost2\r\n$4\r\n1235\r\n$6\r\nmykey2\r\n$1\r\n6\r\n$3\r\n100\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestMove()
        {
            using (var mock = new FakeRedisSocket(":1\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.Move("test", 5));
                Assert.Equal("*3\r\n$4\r\nMOVE\r\n$4\r\ntest\r\n$1\r\n5\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestObject()
        {
            using (var mock = new FakeRedisSocket(":9999\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(9999, redis.ObjectIdleTime("test2"));
                Assert.Equal("*3\r\n$6\r\nOBJECT\r\n$8\r\nIDLETIME\r\n$5\r\ntest2\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void Persist()
        {
            using (var mock = new FakeRedisSocket(":1\r\n", ":0\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.Persist("test1"));
                Assert.Equal("*2\r\n$7\r\nPERSIST\r\n$5\r\ntest1\r\n", mock.GetMessage());
                Assert.False(redis.Persist("test2"));
                Assert.Equal("*2\r\n$7\r\nPERSIST\r\n$5\r\ntest2\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void PExpire()
        {
            using (var mock = new FakeRedisSocket(":1\r\n", ":0\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.PExpire("test1", TimeSpan.FromMilliseconds(5000)));
                Assert.Equal("*3\r\n$7\r\nPEXPIRE\r\n$5\r\ntest1\r\n$4\r\n5000\r\n", mock.GetMessage());
                Assert.False(redis.PExpire("test2", 6000));
                Assert.Equal("*3\r\n$7\r\nPEXPIRE\r\n$5\r\ntest2\r\n$4\r\n6000\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void PExpireAt()
        {
            using (var mock = new FakeRedisSocket(":1\r\n", ":0\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.PExpireAt("test1", new DateTime(2014, 6, 13, 7, 6, 48, 123)));
                Assert.Equal("*3\r\n$9\r\nPEXPIREAT\r\n$5\r\ntest1\r\n$13\r\n1402614408123\r\n", mock.GetMessage());
                Assert.False(redis.PExpireAt("test2", 1402643208123));
                Assert.Equal("*3\r\n$9\r\nPEXPIREAT\r\n$5\r\ntest2\r\n$13\r\n1402643208123\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestPttl()
        {
            using (var mock = new FakeRedisSocket(":123\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(123, redis.PTtl("test"));
                Assert.Equal("*2\r\n$4\r\nPTTL\r\n$4\r\ntest\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestRandomKey()
        {
            using (var mock = new FakeRedisSocket("$7\r\nsomekey\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("somekey", redis.RandomKey());
                Assert.Equal("*1\r\n$9\r\nRANDOMKEY\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestRename()
        {
            using (var mock = new FakeRedisSocket("+OK\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("OK", redis.Rename("test1", "test2"));
                Assert.Equal("*3\r\n$6\r\nRENAME\r\n$5\r\ntest1\r\n$5\r\ntest2\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestRenameNx()
        {
            using (var mock = new FakeRedisSocket(":1\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.True(redis.RenameNx("test1", "test2"));
                Assert.Equal("*3\r\n$8\r\nRENAMENX\r\n$5\r\ntest1\r\n$5\r\ntest2\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestRestore()
        {
            using (var mock = new FakeRedisSocket("+OK\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal("OK", redis.Restore("test", 123, "abc"));
                Assert.Equal("*4\r\n$7\r\nRESTORE\r\n$4\r\ntest\r\n$3\r\n123\r\n$3\r\nabc\r\n", mock.GetMessage());
            }
        }

        [Fact]
        public void TestTtl()
        {
            using (var mock = new FakeRedisSocket(":123\r\n"))
            using (var redis = new PoolRedisClient(mock, new DnsEndPoint("fakehost", 9999)))
            {
                Assert.Equal(123, redis.Ttl("test"));
                Assert.Equal("*2\r\n$3\r\nTTL\r\n$4\r\ntest\r\n", mock.GetMessage());
            }
        }
    }
}
