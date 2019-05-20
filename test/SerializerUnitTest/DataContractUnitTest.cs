using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sino.Serializer.DataContract;
using Xunit;

namespace SerializerUnitTest
{
    public class DataContractUnitTest : BaseUnitTest
    {
        public DataContractUnitTest()
            : base(x =>
            {
                x.AddDataContractSerializer()
                .SetDefaultConvertProvider(DataContractConvertProvider.PROVIDER_NAME);
            })
        { }

        [Fact]
        public void TestDataContractBinaryConvert()
        {
            var convert = Factory.GetConvertProvider(DataContractBinaryConvertProvider.PROVIDER_NAME);

            var res = new DataContractCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var bytes = convert.SerializeByte(res);

            Assert.NotNull(bytes);
            Assert.Equal(164, bytes.Length);

            var obj = convert.DeserializeByte<DataContractCacheItem>(bytes);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public async Task TestDataContractBinaryConvertAsync()
        {
            var convert = Factory.GetConvertProvider(DataContractBinaryConvertProvider.PROVIDER_NAME);

            var res = new DataContractCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var bytes = await convert.SerializeByteAsync(res);

            Assert.NotNull(bytes);
            Assert.Equal(164, bytes.Length);

            var obj = await convert.DeserializeByteAsync<DataContractCacheItem>(bytes);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public void TestDataContractConvert()
        {
            var convert = Factory.GetConvertProvider(DataContractConvertProvider.PROVIDER_NAME);

            var res = new DataContractCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = convert.Serialize(res);

            Assert.NotNull(str);
            Assert.Equal("<DataContractCacheItem xmlns=\"http://schemas.datacontract.org/2004/07/Sino.Serializer.DataContract\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><key>test</key><value>value</value></DataContractCacheItem>", str);

            var obj = convert.Deserialize<DataContractCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public async Task TestDataContractConvertTest()
        {
            var convert = Factory.GetConvertProvider(DataContractConvertProvider.PROVIDER_NAME);

            var res = new DataContractCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = await convert.SerializeAsync(res);

            Assert.NotNull(str);
            Assert.Equal("<DataContractCacheItem xmlns=\"http://schemas.datacontract.org/2004/07/Sino.Serializer.DataContract\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\"><key>test</key><value>value</value></DataContractCacheItem>", str);

            var obj = await convert.DeserializeAsync<DataContractCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public void TestDataContractJsonConvert()
        {
            var convert = Factory.GetConvertProvider(DataContractJsonConvertProvider.PROVIDER_NAME);

            var res = new DataContractCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = convert.Serialize(res);

            Assert.NotNull(str);
            Assert.Equal("{\"key\":\"test\",\"value\":\"value\"}", str);

            var obj = convert.Deserialize<DataContractCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public async Task TestDataContractJsonConvertAsync()
        {
            var convert = Factory.GetConvertProvider(DataContractJsonConvertProvider.PROVIDER_NAME);

            var res = new DataContractCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = await convert.SerializeAsync(res);

            Assert.NotNull(str);
            Assert.Equal("{\"key\":\"test\",\"value\":\"value\"}", str);

            var obj = await convert.DeserializeAsync<DataContractCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public void TestDataContractGzJsonConvert()
        {
            var convert = Factory.GetConvertProvider(DataContractGzJsonConvertProvider.PROVIDER_NAME);

            var res = new DataContractCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var bytes = convert.SerializeByte(res);

            Assert.NotNull(bytes);
            Assert.Equal(50, bytes.Length);

            var obj = convert.DeserializeByte<DataContractCacheItem>(bytes);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public async Task TestDataContractGzJsonConvertAsync()
        {
            var convert = Factory.GetConvertProvider(DataContractGzJsonConvertProvider.PROVIDER_NAME);

            var res = new DataContractCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var bytes = await convert.SerializeByteAsync(res);

            Assert.NotNull(bytes);
            Assert.Equal(50, bytes.Length);

            var obj = await convert.DeserializeByteAsync<DataContractCacheItem>(bytes);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }
    }
}
