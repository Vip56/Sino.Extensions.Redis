using Microsoft.Extensions.DependencyInjection;
using Sino.Serializer.Abstractions;
using Sino.Serializer.Bond;
using System;
using Xunit;

namespace SerializerUnitTest
{
    public class BondUnitTest
    {
        IServiceProvider _provider;
        IConvertProviderFactory _factory;

        public BondUnitTest()
        {
            var servies = new ServiceCollection();
            _provider = servies.AddSinoSerializer(x =>
            {
                x.AddBondSerializer()
                .SetDefaultConvertProvider(BondSimpleJsonConvertProvider.PROVIDER_NAME);
            }).BuildServiceProvider();
            _factory = _provider.GetService<IConvertProviderFactory>();
        }

        [Fact]
        public void TestBinaryConvert()
        {
            var convert = _factory.GetConvertProvider(BondCompactBinaryConvertProvider.PROVIDER_NAME);


        }
    }
}
