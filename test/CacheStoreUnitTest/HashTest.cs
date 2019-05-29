using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CacheStoreUnitTest
{
    public class HashTest : BaseUnitTest
    {
        [Fact]
        public async Task HDelTest()
        {
            await Test(":2\r\n",
                x => x.HDel("test", "test1", "test2"),
                x => x.HDelAsync("test", "test1", "test2"),
                (x, r) =>
                {
                    Assert.Equal(2, r);
                    Assert.Equal("*4\r\n$4\r\nHDEL\r\n$4\r\ntest\r\n$5\r\ntest1\r\n$5\r\ntest2\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task HExistsTest()
        {
            await Test(":1\r\n",
                x => x.HExists("test", "field"),
                x => x.HExistsAsync("test", "field"),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*3\r\n$7\r\nHEXISTS\r\n$4\r\ntest\r\n$5\r\nfield\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task HGetTest()
        {
            await Test("$4\r\ntest\r\n",
                x => x.HGetBytes("test", "field"),
                x => x.HGetBytesAsync("test", "field"),
                (x, r) =>
                {
                    var str = Encoding.UTF8.GetString(r);
                    Assert.Equal("test", str);
                    Assert.Equal("*3\r\n$4\r\nHGET\r\n$4\r\ntest\r\n$5\r\nfield\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task HLenTest()
        {
            await Test(":5\r\n",
                x => x.HLen("test"),
                x => x.HLenAsync("test"),
                (x, r) =>
                {
                    Assert.Equal(5, r);
                    Assert.Equal("*2\r\n$4\r\nHLEN\r\n$4\r\ntest\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task HSetTest()
        {
            var value = Encoding.UTF8.GetBytes("test1");
            await Test(":1\r\n",
                x => x.HSetBytes("test", "field1", value),
                x => x.HSetBytesAsync("test", "field1", value),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*4\r\n$4\r\nHSET\r\n$4\r\ntest\r\n$6\r\nfield1\r\n$5\r\ntest1\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task HSetWithNoExistedTest()
        {
            var value = Encoding.UTF8.GetBytes("test1");
            await Test(":1\r\n",
                x => x.HSetWithNoExistedBytes("test", "field1", value),
                x => x.HSetWithNoExistedBytesAsync("test", "field1", value),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*4\r\n$6\r\nHSETNX\r\n$4\r\ntest\r\n$6\r\nfield1\r\n$5\r\ntest1\r\n", x.RequestString);
                });
        }
    }
}
