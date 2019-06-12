using Newtonsoft.Json;
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
            var body = new ConvertBody
            {
                Key = "test",
                Value = "testv",
                Number = 24
            };

            var bodyStr = JsonConvert.SerializeObject(body);

            await Test("$4\r\ntest\r\n",
                x => x.HGetBytes("test", "field"),
                x => x.HGetBytesAsync("test", "field"),
                (x, r) =>
                {
                    var str = Encoding.UTF8.GetString(r);
                    Assert.Equal("test", str);
                    Assert.Equal("*3\r\n$4\r\nHGET\r\n$4\r\ntest\r\n$5\r\nfield\r\n", x.RequestString);
                });

            await Test($"${bodyStr.Length}\r\n{bodyStr}\r\n",
                x => x.HGet<ConvertBody>("test", "field"),
                x => x.HGetAsync<ConvertBody>("test", "field"),
                (x, r) =>
                {
                    Assert.NotNull(r);
                    Assert.Equal(body.Key, r.Key);
                    Assert.Equal(body.Value, r.Value);
                    Assert.Equal(body.Number, r.Number);
                    Assert.Equal($"*3\r\n$4\r\nHGET\r\n$4\r\ntest\r\n$5\r\nfield\r\n", x.RequestString);
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

            var body = new ConvertBody
            {
                Key = "test",
                Value = "testv",
                Number = 24
            };

            var bodyStr = JsonConvert.SerializeObject(body);

            await Test(":1\r\n",
                x => x.HSetBytes("test", "field1", value),
                x => x.HSetBytesAsync("test", "field1", value),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*4\r\n$4\r\nHSET\r\n$4\r\ntest\r\n$6\r\nfield1\r\n$5\r\ntest1\r\n", x.RequestString);
                });

            await Test(":1\r\n",
                x => x.HSet("test", "field1", body),
                x => x.HSetAsync("test", "field1", body),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal($"*4\r\n$4\r\nHSET\r\n$4\r\ntest\r\n$6\r\nfield1\r\n${bodyStr.Length}\r\n{bodyStr}\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task HSetWithNoExistedTest()
        {
            var value = Encoding.UTF8.GetBytes("test1");

            var body = new ConvertBody
            {
                Key = "test",
                Value = "testv",
                Number = 24
            };

            var bodyStr = JsonConvert.SerializeObject(body);

            await Test(":1\r\n",
                x => x.HSetWithNoExistedBytes("test", "field1", value),
                x => x.HSetWithNoExistedBytesAsync("test", "field1", value),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal("*4\r\n$6\r\nHSETNX\r\n$4\r\ntest\r\n$6\r\nfield1\r\n$5\r\ntest1\r\n", x.RequestString);
                });

            await Test(":1\r\n",
                x => x.HSetWithNoExisted("test", "field1", body),
                x => x.HSetWithNoExistedAsync("test", "field1", body),
                (x, r) =>
                {
                    Assert.True(r);
                    Assert.Equal($"*4\r\n$6\r\nHSETNX\r\n$4\r\ntest\r\n$6\r\nfield1\r\n${bodyStr.Length}\r\n{bodyStr}\r\n", x.RequestString);
                });
        }
    }
}
