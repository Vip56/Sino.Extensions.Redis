﻿using Microsoft.Extensions.DependencyInjection;
using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerializerUnitTest
{
    public class BaseUnitTest
    {
        protected IServiceProvider Provider { get; set; }

        protected IConvertProviderFactory Factory { get; set; }

        public BaseUnitTest(Action<SerializerSettingsBuilder> setupAction)
        {
            var services = new ServiceCollection();
            Provider = services.AddSinoSerializer(setupAction).BuildServiceProvider();
            Factory = Provider.GetService<IConvertProviderFactory>();
        }
    }
}
