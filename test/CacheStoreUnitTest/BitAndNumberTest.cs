using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CacheStoreUnitTest
{
    public class BitAndNumberTest : BaseUnitTest
    {
        [Fact]
        public async Task BitCountTest()
        {
            await Test(":10\r\n",
                x => x.BitCount("key"),
                x => x.BitCountAsync("key"),
                (x, r) =>
                {
                    Assert.Equal(10, r);
                    Assert.Equal("*2\r\n$8\r\nBITCOUNT\r\n$3\r\nkey\r\n", x.RequestString);
                });

            await Test(":4\r\n",
                x => x.BitCount("key", 0, 1),
                x => x.BitCountAsync("key", 0, 1),
                (x, r) =>
                {
                    Assert.Equal(4, r);
                    Assert.Equal("*4\r\n$8\r\nBITCOUNT\r\n$3\r\nkey\r\n$1\r\n0\r\n$1\r\n1\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task SetBitTest()
        {
            await Test(":1\r\n",
                x => x.SetBit("key", 5, true),
                x => x.SetBitAsync("key", 5, true),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*4\r\n$6\r\nSETBIT\r\n$3\r\nkey\r\n$1\r\n5\r\n$1\r\n1\r\n", x.RequestString);
                });

            await Test(":0\r\n",
                x => x.SetBit("key", 5, false),
                x => x.SetBitAsync("key", 5, false),
                (x, r) =>
                {
                    Assert.False(r);
                    Assert.Equal("*4\r\n$6\r\nSETBIT\r\n$3\r\nkey\r\n$1\r\n5\r\n$1\r\n0\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task GetBitTest()
        {
            await Test(":1\r\n",
                x => x.GetBit("key", 10),
                x => x.GetBitAsync("key", 10),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*3\r\n$6\r\nGETBIT\r\n$3\r\nkey\r\n$2\r\n10\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task DecrTest()
        {
            await Test(":10\r\n",
                x => x.Decr("key"),
                x => x.DecrAsync("key"),
                (x, r) =>
                {
                    Assert.Equal(10, r);
                    Assert.Equal("*2\r\n$4\r\nDECR\r\n$3\r\nkey\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task DecrByTest()
        {
            await Test(":10\r\n",
                x => x.DecrBy("key", 5),
                x => x.DecrByAsync("key", 5),
                (x, r) =>
                {
                    Assert.Equal(10, r);
                    Assert.Equal("*3\r\n$6\r\nDECRBY\r\n$3\r\nkey\r\n$1\r\n5\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task IncrTest()
        {
            await Test(":5\r\n",
                x => x.Incr("key"),
                x => x.IncrAsync("key"),
                (x, r) =>
                {
                    Assert.Equal(5, r);
                    Assert.Equal("*2\r\n$4\r\nINCR\r\n$3\r\nkey\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task IncrByTest()
        {
            await Test(":5\r\n",
                x => x.IncrBy("key", 2),
                x => x.IncrByAsync("key", 2),
                (x, r) =>
                {
                    Assert.Equal(5, r);
                    Assert.Equal("*3\r\n$6\r\nINCRBY\r\n$3\r\nkey\r\n$1\r\n2\r\n", x.RequestString);
                });
        }
    }
}
