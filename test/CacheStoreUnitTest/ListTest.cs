using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CacheStoreUnitTest
{
    public class ListTest : BaseUnitTest
    {
        [Fact]
        public async Task LPopTest()
        {
            await Test("$5\r\ntest1\r\n",
                x => x.LPopBytes("test"),
                x => x.LPopBytesAsync("test"),
                (x, r) =>
                {
                    var str = Encoding.UTF8.GetString(r);
                    Assert.Equal("test1", str);
                    Assert.Equal("*2\r\n$4\r\nLPOP\r\n$4\r\ntest\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task LIndexTest()
        {
            await Test("$5\r\ntest1\r\n",
                x => x.LIndexBytes("test", 0),
                x => x.LIndexBytesAsync("test", 0),
                (x, r) =>
                {
                    var str = Encoding.UTF8.GetString(r);
                    Assert.Equal("test1", str);
                    Assert.Equal("*3\r\n$6\r\nLINDEX\r\n$4\r\ntest\r\n$1\r\n0\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task LLenTest()
        {
            await Test(":3\r\n",
                x => x.LLen("test"),
                x => x.LLenAsync("test"),
                (x, r) =>
                {
                    Assert.Equal(3, r);
                    Assert.Equal("*2\r\n$4\r\nLLEN\r\n$4\r\ntest\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task LPushTest()
        {
            var value = Encoding.UTF8.GetBytes("test1");
            await Test(":1\r\n",
                x => x.LPushBytes("test", value),
                x => x.LPushBytesAsync("test", value),
                (x, r) =>
                {
                    Assert.Equal(1, r);
                    Assert.Equal("*3\r\n$5\r\nLPUSH\r\n$4\r\ntest\r\n$5\r\ntest1\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task RPopTest()
        {
            await Test("$5\r\ntest1\r\n",
                x => x.RPopBytes("test"),
                x => x.RPopBytesAsync("test"),
                (x, r) =>
                {
                    var str = Encoding.UTF8.GetString(r);
                    Assert.Equal("test1", str);
                    Assert.Equal("*2\r\n$4\r\nRPOP\r\n$4\r\ntest\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task RPushTest()
        {
            var value = Encoding.UTF8.GetBytes("test1");
            await Test(":3\r\n",
                x => x.RPushBytes("test", value),
                x => x.RPushBytesAsync("test", value),
                (x, r) =>
                {
                    Assert.Equal(3, r);
                    Assert.Equal("*3\r\n$5\r\nRPUSH\r\n$4\r\ntest\r\n$5\r\ntest1\r\n", x.RequestString);
                });
        }
    }
}
