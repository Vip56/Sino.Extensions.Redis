using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sino.Serializer.Json;
using Xunit;

namespace SerializerUnitTest
{
    public class JsonUnitTest : BaseUnitTest
    {
        public JsonUnitTest()
            : base(x =>
             {
                 x.AddJsonSerializer()
                 .SetDefaultConvertProvider(JsonConvertProvider.PROVIDER_NAME);
             })
        { }

        [Fact]
        public void TestJsonConvert()
        {
            var convert = Factory.GetConvertProvider(JsonConvertProvider.PROVIDER_NAME);

            var res = new TestResponse
            {
                Field = "test",
                Field2 = 12,
                Fiedl3 = 1234L,
                Field4 = 1,
                Field5 = DateTime.Parse("2019-05-21T09:02:33.7544554+08:00"),
                Field6 = 123.213f,
                Field7 = 123.123123,
                Field8 = new List<string> { "1", "2" },
                Field9 = new List<int> { 1, 2 },
                Field10 = new Child
                {
                    Field = "123213"
                }
            };
            var str = convert.Serialize(res);

            Assert.NotNull(str);
            Assert.Equal("{\"Field\":\"test\",\"Field2\":12,\"Fiedl3\":1234,\"Field4\":1,\"Field5\":\"2019-05-21T09:02:33.7544554+08:00\",\"Field6\":123.213,\"Field7\":123.123123,\"Field8\":[\"1\",\"2\"],\"Field9\":[1,2],\"Field10\":{\"Field\":\"123213\"}}", str);


            var obj = convert.Deserialize<TestResponse>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Field);
            Assert.Equal(12, obj.Field2);
            Assert.Equal(1234L, obj.Fiedl3);
            Assert.Equal(1, obj.Field4);
            Assert.Equal(123.213f, obj.Field6);
            Assert.Equal(123.123123, obj.Field7);
            Assert.Equal(2, obj.Field8.Count);
            Assert.Equal(2, obj.Field9.Count);
            Assert.Equal("123213", obj.Field10.Field);
        }

        [Fact]
        public void TestGzJsonConvert()
        {
            var convert = Factory.GetConvertProvider(GzJsonConvertProvider.PROVIDER_NAME);

            var res = new TestResponse
            {
                Field = "test",
                Field2 = 12,
                Fiedl3 = 1234L,
                Field4 = 1,
                Field5 = DateTime.Parse("2019-05-21T09:02:33.7544554+08:00"),
                Field6 = 123.213f,
                Field7 = 123.123123,
                Field8 = new List<string> { "1", "2" },
                Field9 = new List<int> { 1, 2 },
                Field10 = new Child
                {
                    Field = "123213"
                }
            };
            var bytes = convert.SerializeByte(res);

            Assert.NotNull(bytes);
            Assert.Equal(137, bytes.Length);

            var obj = convert.DeserializeByte<TestResponse>(bytes);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Field);
            Assert.Equal(12, obj.Field2);
            Assert.Equal(1234L, obj.Fiedl3);
            Assert.Equal(1, obj.Field4);
            Assert.Equal(123.213f, obj.Field6);
            Assert.Equal(123.123123, obj.Field7);
            Assert.Equal(2, obj.Field8.Count);
            Assert.Equal(2, obj.Field9.Count);
            Assert.Equal("123213", obj.Field10.Field);
        }

        [Fact]
        public async Task TestJsonConvertAsync()
        {
            var convert = Factory.GetConvertProvider(JsonConvertProvider.PROVIDER_NAME);

            var res = new TestResponse
            {
                Field = "test",
                Field2 = 12,
                Fiedl3 = 1234L,
                Field4 = 1,
                Field5 = DateTime.Parse("2019-05-21T09:02:33.7544554+08:00"),
                Field6 = 123.213f,
                Field7 = 123.123123,
                Field8 = new List<string> { "1", "2" },
                Field9 = new List<int> { 1, 2 },
                Field10 = new Child
                {
                    Field = "123213"
                }
            };
            var str = await convert.SerializeAsync(res);

            Assert.NotNull(str);
            Assert.Equal("{\"Field\":\"test\",\"Field2\":12,\"Fiedl3\":1234,\"Field4\":1,\"Field5\":\"2019-05-21T09:02:33.7544554+08:00\",\"Field6\":123.213,\"Field7\":123.123123,\"Field8\":[\"1\",\"2\"],\"Field9\":[1,2],\"Field10\":{\"Field\":\"123213\"}}", str);


            var obj = await convert.DeserializeAsync<TestResponse>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Field);
            Assert.Equal(12, obj.Field2);
            Assert.Equal(1234L, obj.Fiedl3);
            Assert.Equal(1, obj.Field4);
            Assert.Equal(123.213f, obj.Field6);
            Assert.Equal(123.123123, obj.Field7);
            Assert.Equal(2, obj.Field8.Count);
            Assert.Equal(2, obj.Field9.Count);
            Assert.Equal("123213", obj.Field10.Field);
        }

        [Fact]
        public async Task TestGzJsonConvertAsync()
        {
            var convert = Factory.GetConvertProvider(GzJsonConvertProvider.PROVIDER_NAME);

            var res = new TestResponse
            {
                Field = "test",
                Field2 = 12,
                Fiedl3 = 1234L,
                Field4 = 1,
                Field5 = DateTime.Parse("2019-05-21T09:02:33.7544554+08:00"),
                Field6 = 123.213f,
                Field7 = 123.123123,
                Field8 = new List<string> { "1", "2" },
                Field9 = new List<int> { 1, 2 },
                Field10 = new Child
                {
                    Field = "123213"
                }
            };
            var bytes = await convert.SerializeByteAsync(res);

            Assert.NotNull(bytes);
            Assert.Equal(137, bytes.Length);

            var obj = await convert.DeserializeByteAsync<TestResponse>(bytes);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Field);
            Assert.Equal(12, obj.Field2);
            Assert.Equal(1234L, obj.Fiedl3);
            Assert.Equal(1, obj.Field4);
            Assert.Equal(123.213f, obj.Field6);
            Assert.Equal(123.123123, obj.Field7);
            Assert.Equal(2, obj.Field8.Count);
            Assert.Equal(2, obj.Field9.Count);
            Assert.Equal("123213", obj.Field10.Field);
        }
    }
}
