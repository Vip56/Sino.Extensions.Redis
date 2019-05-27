using System;
using System.Threading.Tasks;
using Xunit;

namespace CacheStoreUnitTest
{
    public class ConnectionTest : BaseUnitTest
    {
        [Fact]
        public async Task PingTest()
        {
            await Test("+PONG\r\n",
                x => x.Ping(),
                x => x.PingAsync(),
                (x, r) =>
                {
                    Assert.Equal("PONG", r);
                    Assert.Equal("*1\r\n$4\r\nPING\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task QuitTest()
        {
            await Test("+OK\r\n",
                x => x.Quit(),
                x => x.QuitAsync(),
                (x, r) =>
                {
                    Assert.Equal("OK", r);
                    Assert.Equal("*1\r\n$4\r\nQUIT\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task SelectTest()
        {
            await Test("+OK\r\n",
                x => x.Select("2"),
                x => x.SelectAsync("2"),
                (x, r) =>
                {
                    Assert.Equal("OK", r);
                    Assert.Equal("*2\r\n$6\r\nSELECT\r\n$1\r\n2\r\n", x.RequestString);
                });
        }
    }
}
