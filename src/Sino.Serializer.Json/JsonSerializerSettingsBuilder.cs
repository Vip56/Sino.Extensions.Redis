using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.Json
{
    public static class JsonSerializerSettingsBuilder
    {
        public const string PROVIDER_GROUP = "json_";

        public static SerializerSettingsBuilder AddJsonSerializer(this SerializerSettingsBuilder settings)
        {
            return settings;
        }
    }
}
