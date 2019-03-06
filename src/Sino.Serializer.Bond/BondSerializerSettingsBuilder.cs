using Sino.Serializer.Abstractions;
using Sino.Serializer.Bond;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 序列化注入
    /// </summary>
    public static class BondSerializerSettingsBuilder
    {
        public static SerializerSettingsBuilder AddBondSerializer(this SerializerSettingsBuilder settings)
        {
            return settings.AddBondCompactBinarySerializer()
                .AddBondFastBinarySerializer()
                .AddBondSerializer()
                .AddBondSimpleJsonSerializer();
        }

        /// <summary>
        /// 添加紧凑二进制序列化，默认采用UTF-8编码
        /// <param name="encoding">编码格式（可选）</param>
        /// </summary>
        public static SerializerSettingsBuilder AddBondCompactBinarySerializer(this SerializerSettingsBuilder settings, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;
            settings.AddProvider(BondCompactBinaryConvertProvider.PROVIDER_NAME, () =>
            {
                return new BondCompactBinaryConvertProvider(encoding);
            });
            return settings;
        }

        /// <summary>
        /// 添加快速二进制序列化，默认采用UTF-8编码
        /// <param name="encoding">编码格式（可选）</param>
        /// </summary>
        public static SerializerSettingsBuilder AddBondFastBinarySerializer(this SerializerSettingsBuilder settings, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;
            settings.AddProvider(BondFastBinaryConvertProvider.PROVIDER_NAME, () =>
            {
                return new BondFastBinaryConvertProvider(encoding);
            });
            return settings;
        }

        /// <summary>
        /// 添加基本Json序列化，默认采用UTF-8编码
        /// <param name="encoding">编码格式（可选）</param>
        /// </summary>
        public static SerializerSettingsBuilder AddBondSimpleJsonSerializer(this SerializerSettingsBuilder settings, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;
            settings.AddProvider(BondSimpleJsonConvertProvider.PROVIDER_NAME, () =>
            {
                return new BondSimpleJsonConvertProvider(encoding);
            });
            return settings;
        }
    }
}
