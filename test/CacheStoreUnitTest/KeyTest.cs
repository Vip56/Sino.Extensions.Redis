using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CacheStoreUnitTest
{
    public class KeyTest : BaseUnitTest
    {
        [Fact]
        public async Task ExistsTest()
        {
            await Test(":1\r\n",
                x => x.Exists("test1"),
                x => x.ExistsAsync("test1"),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*2\r\n$6\r\nEXISTS\r\n$5\r\ntest1\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task GetTest()
        {
            await Test("$5\r\nhello\r\n",
                x => x.GetBytes("key"),
                x => x.GetBytesAsync("key"),
                (x, r) =>
                {
                    var key = Encoding.UTF8.GetString(r);
                    Assert.Equal("hello", key);
                    Assert.Equal("*2\r\n$3\r\nGET\r\n$3\r\nkey\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task SetTest()
        {
            var value = Encoding.UTF8.GetBytes("value");
            await Test("+OK\r\n",
                x => x.SetBytes("key", value),
                x => x.SetBytesAsync("key", value),
                (x, r) =>
                {
                    Assert.Equal("OK", r);
                    Assert.Equal("*3\r\n$3\r\nSET\r\n$3\r\nkey\r\n$5\r\nvalue\r\n", x.RequestString);
                });

            await Test("+OK\r\n",
                x => x.SetBytes("key", value, 1),
                x => x.SetBytesAsync("key", value, 1),
                (x, r) =>
                {
                    Assert.Equal("OK", r);
                    Assert.Equal("*5\r\n$3\r\nSET\r\n$3\r\nkey\r\n$5\r\nvalue\r\n$2\r\nEX\r\n$1\r\n1\r\n", x.RequestString);
                });

            await Test("$-1\r\n",
                x => x.SetBytes("key", value, null, 1),
                x => x.SetBytesAsync("key", value, null, 1),
                (x, r) =>
                {
                    Assert.Null(r);
                    Assert.Equal("*5\r\n$3\r\nSET\r\n$3\r\nkey\r\n$5\r\nvalue\r\n$2\r\nPX\r\n$1\r\n1\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task SetWithNoExistedTest()
        {
            var value = Encoding.UTF8.GetBytes("value");
            await Test("+OK\r\n",
                x => x.SetWithNoExistedBytes("key", value, 1),
                x => x.SetWithNoExistedBytesAsync("key", value, 1),
                (x, r) =>
                {
                    Assert.Equal("OK", r);
                    Assert.Equal("*6\r\n$3\r\nSET\r\n$3\r\nkey\r\n$5\r\nvalue\r\n$2\r\nEX\r\n$1\r\n1\r\n$2\r\nNX\r\n", x.RequestString);
                });

            await Test("$-1\r\n",
                x => x.SetWithNoExistedBytes("key", value, null, 1),
                x => x.SetWithNoExistedBytesAsync("key", value, null, 1),
                (x, r) =>
                {
                    Assert.Null(r);
                    Assert.Equal("*6\r\n$3\r\nSET\r\n$3\r\nkey\r\n$5\r\nvalue\r\n$2\r\nPX\r\n$1\r\n1\r\n$2\r\nNX\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task ExpireTest()
        {
            await Test(":1\r\n",
                x => x.Expire("test1", (long)TimeSpan.FromSeconds(10).TotalSeconds),
                x => x.ExpireAsync("test1", (long)TimeSpan.FromSeconds(10).TotalSeconds),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*3\r\n$6\r\nEXPIRE\r\n$5\r\ntest1\r\n$2\r\n10\r\n", x.RequestString);
                });

            await Test(":0\r\n",
                x => x.Expire("test2", 20L),
                x => x.ExpireAsync("test2", 20L),
                (x, r) =>
                {
                    Assert.False(r);
                    Assert.Equal("*3\r\n$6\r\nEXPIRE\r\n$5\r\ntest2\r\n$2\r\n20\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task RemoveTest()
        {
            await Test(":3\r\n",
                x => x.Remove("test"),
                x => x.RemoveAsync("test"),
                (x, r) =>
                {
                    Assert.Equal(3, r);
                    Assert.Equal("*2\r\n$3\r\nDEL\r\n$4\r\ntest\r\n", x.RequestString);
                });
        }
    }
}
