using Sino.Serializer.Abstractions;
using Sino.Serializer.DataContract;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 序列化注入
    /// </summary>
    public static class DataContractSerializerSettingsBuilder
    {
        public const string PROVIDER_GROUP = "datacontract_";

        public static SerializerSettingsBuilder AddDataContractSerializer(this SerializerSettingsBuilder settings)
        {
            return settings.AddDataContractBinarySerializer()
                .AddDataContractNormalSerializer()
                .AddDataContractGzJsonSerializer()
                .AddDataContractJsonSerializer();
        }

        /// <summary>
        /// 添加XML字节序列化，默认采用UTF-8编码
        /// </summary>
        /// <param name="dcSettings">配置（可选）</param>
        /// <param name="encoding">编码格式（可选）</param>
        public static SerializerSettingsBuilder AddDataContractBinarySerializer(this SerializerSettingsBuilder settings, DataContractSerializerSettings dcSettings = null, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;

            settings.AddProvider(PROVIDER_GROUP + DataContractBinaryConvertProvider.PROVIDER_NAME, () =>
            {
                return new DataContractBinaryConvertProvider(encoding, dcSettings);
            });
            return settings;
        }

        /// <summary>
        /// 添加XML序列化，默认采用UTF-8编码
        /// </summary>
        /// <param name="dcSettings">配置（可选）</param>
        /// <param name="encoding">编码格式（可选）</param>
        public static SerializerSettingsBuilder AddDataContractNormalSerializer(this SerializerSettingsBuilder settings, DataContractSerializerSettings dcSettings = null, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;

            settings.AddProvider(PROVIDER_GROUP + DataContractConvertProvider.PROVIDER_NAME, () =>
            {
                return new DataContractConvertProvider(encoding, dcSettings);
            });
            return settings;
        }

        /// <summary>
        /// 添加压缩Json序列化，默认采用UTF-8编码
        /// </summary>
        /// <param name="dcSettings">配置（可选）</param>
        /// <param name="encoding">编码格式（可选）</param>
        public static SerializerSettingsBuilder AddDataContractGzJsonSerializer(this SerializerSettingsBuilder settings, DataContractJsonSerializerSettings dcSettings = null, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;

            settings.AddProvider(PROVIDER_GROUP + DataContractGzJsonConvertProvider.PROVIDER_NAME, () =>
            {
                return new DataContractGzJsonConvertProvider(encoding, dcSettings);
            });
            return settings;
        }

        /// <summary>
        /// 添加Json序列化，默认采用UTF-8编码
        /// </summary>
        /// <param name="dcSettings">配置（可选）</param>
        /// <param name="encoding">编码格式（可选）</param>
        public static SerializerSettingsBuilder AddDataContractJsonSerializer(this SerializerSettingsBuilder settings, DataContractJsonSerializerSettings dcSettings = null, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;

            settings.AddProvider(PROVIDER_GROUP + DataContractJsonConvertProvider.PROVIDER_NAME, () =>
            {
                return new DataContractJsonConvertProvider(encoding, dcSettings);
            });
            return settings;
        }
    }
}
