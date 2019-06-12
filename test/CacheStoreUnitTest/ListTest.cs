using Newtonsoft.Json;
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
            var body = new ConvertBody
            {
                Key = "test",
                Value = "testv",
                Number = 24
            };

            var bodyStr = JsonConvert.SerializeObject(body);

            await Test("$5\r\ntest1\r\n",
                x => x.LPopBytes("test"),
                x => x.LPopBytesAsync("test"),
                (x, r) =>
                {
                    var str = Encoding.UTF8.GetString(r);
                    Assert.Equal("test1", str);
                    Assert.Equal("*2\r\n$4\r\nLPOP\r\n$4\r\ntest\r\n", x.RequestString);
                });

            await Test($"${bodyStr.Length}\r\n{bodyStr}\r\n",
                x => x.LPop<ConvertBody>("test"),
                x => x.LPopAsync<ConvertBody>("test"),
                (x, r) =>
                {
                    Assert.NotNull(r);
                    Assert.Equal(body.Key, r.Key);
                    Assert.Equal(body.Value, r.Value);
                    Assert.Equal(body.Number, r.Number);
                    Assert.Equal("*2\r\n$4\r\nLPOP\r\n$4\r\ntest\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task LIndexTest()
        {
            var body = new ConvertBody
            {
                Key = "test",
                Value = "testv",
                Number = 24
            };

            var bodyStr = JsonConvert.SerializeObject(body);

            await Test("$5\r\ntest1\r\n",
                x => x.LIndexBytes("test", 0),
                x => x.LIndexBytesAsync("test", 0),
                (x, r) =>
                {
                    var str = Encoding.UTF8.GetString(r);
                    Assert.Equal("test1", str);
                    Assert.Equal("*3\r\n$6\r\nLINDEX\r\n$4\r\ntest\r\n$1\r\n0\r\n", x.RequestString);
                });

            await Test($"${bodyStr.Length}\r\n{bodyStr}\r\n",
                x => x.LIndex<ConvertBody>("test", 0),
                x => x.LIndexAsync<ConvertBody>("test", 0),
                (x, r) =>
                {
                    Assert.NotNull(r);
                    Assert.Equal(body.Key, r.Key);
                    Assert.Equal(body.Value, r.Value);
                    Assert.Equal(body.Number, r.Number);
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

            var body = new ConvertBody
            {
                Key = "test",
                Value = "testv",
                Number = 24
            };

            var bodyStr = JsonConvert.SerializeObject(body);

            await Test(":1\r\n",
                x => x.LPushBytes("test", value),
                x => x.LPushBytesAsync("test", value),
                (x, r) =>
                {
                    Assert.Equal(1, r);
                    Assert.Equal("*3\r\n$5\r\nLPUSH\r\n$4\r\ntest\r\n$5\r\ntest1\r\n", x.RequestString);
                });

            await Test(":1\r\n",
                x => x.LPush("test", body),
                x => x.LPushAsync("test", body),
                (x, r) =>
                {
                    Assert.Equal(1, r);
                    Assert.Equal($"*3\r\n$5\r\nLPUSH\r\n$4\r\ntest\r\n${bodyStr.Length}\r\n{bodyStr}\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task RPopTest()
        {
            var body = new ConvertBody
            {
                Key = "test",
                Value = "testv",
                Number = 24
            };

            var bodyStr = JsonConvert.SerializeObject(body);

            await Test("$5\r\ntest1\r\n",
                x => x.RPopBytes("test"),
                x => x.RPopBytesAsync("test"),
                (x, r) =>
                {
                    var str = Encoding.UTF8.GetString(r);
                    Assert.Equal("test1", str);
                    Assert.Equal("*2\r\n$4\r\nRPOP\r\n$4\r\ntest\r\n", x.RequestString);
                });

            await Test($"${bodyStr.Length}\r\n{bodyStr}\r\n",
                x => x.RPop<ConvertBody>("test"),
                x => x.RPopAsync<ConvertBody>("test"),
                (x, r) =>
                {
                    Assert.NotNull(r);
                    Assert.Equal(body.Key, r.Key);
                    Assert.Equal(body.Value, r.Value);
                    Assert.Equal(body.Number, r.Number);
                    Assert.Equal("*2\r\n$4\r\nRPOP\r\n$4\r\ntest\r\n", x.RequestString);
                });
        }

        [Fact]
        public async Task RPushTest()
        {
            var value = Encoding.UTF8.GetBytes("test1");

            var body = new ConvertBody
            {
                Key = "test",
                Value = "testv",
                Number = 24
            };

            var bodyStr = JsonConvert.SerializeObject(body);

            await Test(":3\r\n",
                x => x.RPushBytes("test", value),
                x => x.RPushBytesAsync("test", value),
                (x, r) =>
                {
                    Assert.Equal(3, r);
                    Assert.Equal("*3\r\n$5\r\nRPUSH\r\n$4\r\ntest\r\n$5\r\ntest1\r\n", x.RequestString);
                });

            await Test(":3\r\n",
                x => x.RPush("test", body),
                x => x.RPushAsync("test", body),
                (x, r) =>
                {
                    Assert.Equal(3, r);
                    Assert.Equal($"*3\r\n$5\r\nRPUSH\r\n$4\r\ntest\r\n${bodyStr.Length}\r\n{bodyStr}\r\n", x.RequestString);
                });
        }
    }
}
