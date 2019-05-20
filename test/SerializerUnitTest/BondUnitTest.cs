using Microsoft.Extensions.DependencyInjection;
using Sino.Serializer.Abstractions;
using Sino.Serializer.Bond;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SerializerUnitTest
{
    public class BondUnitTest : BaseUnitTest
    {
        public BondUnitTest()
            : base(x =>
            {
                x.AddBondSerializer()
                .SetDefaultConvertProvider(BondSimpleJsonConvertProvider.PROVIDER_NAME);
            })
        { }

        [Fact]
        public void TestBinaryConvert()
        {
            var convert = Factory.GetConvertProvider(BondCompactBinaryConvertProvider.PROVIDER_NAME);

            var res = new BondCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = convert.Serialize(res);

            Assert.NotNull(str);
            Assert.Equal(")\u0004testI\u0005value\0", str);

            var obj = convert.Deserialize<BondCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public async Task TestBinaryConvertAsync()
        {
            var convert = Factory.GetConvertProvider(BondCompactBinaryConvertProvider.PROVIDER_NAME);

            var res = new BondCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = await convert.SerializeAsync(res);

            Assert.NotNull(str);
            Assert.Equal(")\u0004testI\u0005value\0", str);

            var obj = await convert.DeserializeAsync<BondCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public void TestFastBinaryConvert()
        {
            var convert = Factory.GetConvertProvider(BondFastBinaryConvertProvider.PROVIDER_NAME);

            var res = new BondCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = convert.Serialize(res);

            Assert.NotNull(str);
            Assert.Equal("\t\u0001\0\u0004test\t\u0002\0\u0005value\0", str);

            var obj = convert.Deserialize<BondCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public async Task TestFastBinaryConvertAsync()
        {
            var convert = Factory.GetConvertProvider(BondFastBinaryConvertProvider.PROVIDER_NAME);

            var res = new BondCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = await convert.SerializeAsync(res);

            Assert.NotNull(str);
            Assert.Equal("\t\u0001\0\u0004test\t\u0002\0\u0005value\0", str);

            var obj = await convert.DeserializeAsync<BondCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public void TestSimpleJsonConvert()
        {
            var convert = Factory.GetConvertProvider(BondSimpleJsonConvertProvider.PROVIDER_NAME);

            var res = new BondCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = convert.Serialize(res);

            Assert.NotNull(str);
            Assert.Equal("{\"Key\":\"test\",\"Value\":\"value\"}", str);

            var obj = convert.Deserialize<BondCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }

        [Fact]
        public async Task TestSimpleJsonConvertAsync()
        {
            var convert = Factory.GetConvertProvider(BondSimpleJsonConvertProvider.PROVIDER_NAME);

            var res = new BondCacheItem
            {
                Key = "test",
                Value = "value"
            };
            var str = await convert.SerializeAsync(res);

            Assert.NotNull(str);
            Assert.Equal("{\"Key\":\"test\",\"Value\":\"value\"}", str);

            var obj = await convert.DeserializeAsync<BondCacheItem>(str);

            Assert.NotNull(obj);
            Assert.Equal("test", obj.Key);
            Assert.Equal("value", obj.Value);
        }
    }
}
