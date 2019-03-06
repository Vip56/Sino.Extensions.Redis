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

            var t = DateTime.Now;
            TestResponse res = new TestResponse
            {
                Field = "test",
                Field2 = int.MaxValue,
                Fiedl3 = long.MaxValue,
                Field4 = 0xFF,
                Field5 = t,
                Field6 = float.MaxValue,
                Field7 = double.MaxValue,
                Field8 = new List<string>
                {
                    "test1",
                    "test2",
                    "test3"
                },
                Field9 = new List<int>
                {
                    1,
                    2,
                    3,
                    4
                },
                Field10 = new Child
                {
                    Field = "childtest"
                }
            };

            var str = convert.Serialize(res);

            Assert.NotNull(str);
            Assert.True(str.Length > 0);

            var obj = convert.Deserialize<TestResponse>(str);

        }
    }
}
