using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.Bond
{
    public static class BondSerializerSettingsBuilder
    {
        public const string PROVIDER_GROUP = "bond_";

        public static SerializerSettingsBuilder AddBondSerializer(this SerializerSettingsBuilder settings)
        {
            return settings;
        }

        /// <summary>
        /// 添加紧凑二进制序列化，默认采用UTF-8编码
        /// </summary>
        public static SerializerSettingsBuilder AddBondCompactBinarySerializer(this SerializerSettingsBuilder settings)
        {
            settings.AddBondCompactBinarySerializer(settings.GlobalEncoding);
            return settings;
        }

        /// <summary>
        /// 添加紧凑二进制序列化
        /// </summary>
        /// <param name="encoding">编码格式</param>
        public static SerializerSettingsBuilder AddBondCompactBinarySerializer(this SerializerSettingsBuilder settings, Encoding encoding)
        {
            settings.AddProvider(PROVIDER_GROUP + BondCompactBinaryConvertProvider.PROVIDER_NAME, () =>
            {
                return new BondCompactBinaryConvertProvider(encoding);
            });
            return settings;
        }

        /// <summary>
        /// 添加快速二进制序列化，默认采用UTF-8编码
        /// </summary>
        public static SerializerSettingsBuilder AddBondFastBinarySerializer(this SerializerSettingsBuilder settings)
        {
            settings.AddBondFastBinarySerializer(settings.GlobalEncoding);
            return settings;
        }

        /// <summary>
        /// 添加快速二进制序列化
        /// </summary>
        /// <param name="encoding">编码格式</param>
        public static SerializerSettingsBuilder AddBondFastBinarySerializer(this SerializerSettingsBuilder settings, Encoding encoding)
        {
            settings.AddProvider(PROVIDER_GROUP + BondFastBinaryConvertProvider.PROVIDER_NAME, () =>
            {
                return new BondFastBinaryConvertProvider(encoding);
            });
            return settings;
        }

        /// <summary>
        /// 添加基本Json序列化
        /// </summary>
        public static SerializerSettingsBuilder AddBondSimpleJsonSerializer(this SerializerSettingsBuilder settings)
        {
            settings.AddBondSimpleJsonSerializer(settings.GlobalEncoding);
            return settings;
        }

        /// <summary>
        /// 添加基本Json序列化
        /// </summary>
        /// <param name="encoding">编码格式</param>
        public static SerializerSettingsBuilder AddBondSimpleJsonSerializer(this SerializerSettingsBuilder settings, Encoding encoding)
        {
            settings.AddProvider(PROVIDER_GROUP + BondSimpleJsonConvertProvider.PROVIDER_NAME, () =>
            {
                return new BondSimpleJsonConvertProvider(encoding);
            });
            return settings;
        }
    }
}
