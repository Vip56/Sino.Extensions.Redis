using Microsoft.Extensions.DependencyInjection;
using Sino.Serializer.Abstractions;
using Sino.Serializer.Bond;
using System;
using System.Collections.Generic;
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
            Assert.True(str.Length > 0);

            var obj = convert.Deserialize<TestResponse>(str);

        }
    }
}
