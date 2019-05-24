using System;
using System.Threading.Tasks;
using Xunit;

namespace CacheStoreUnitTest
{
    public class ConnecctionTest : BaseUnitTest
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
    }
}
