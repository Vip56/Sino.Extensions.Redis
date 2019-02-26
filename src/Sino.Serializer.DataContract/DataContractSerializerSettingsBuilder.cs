using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.DataContract
{
    public static class DataContractSerializerSettingsBuilder
    {
        public const string PROVIDER_GROUP = "datacontract_";

        public static SerializerSettingsBuilder AddDataContractSerializer(this SerializerSettingsBuilder settings)
        {
            return settings;
        }
    }
}
