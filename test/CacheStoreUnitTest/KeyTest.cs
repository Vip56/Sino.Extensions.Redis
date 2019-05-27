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

        }
    }
}
