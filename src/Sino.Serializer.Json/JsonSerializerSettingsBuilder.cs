using Newtonsoft.Json;
using Sino.Serializer.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.Json
{
    /// <summary>
    /// 序列化注入
    /// </summary>
    public static class JsonSerializerSettingsBuilder
    {
        public const string PROVIDER_GROUP = "json_";

        public static SerializerSettingsBuilder AddJsonSerializer(this SerializerSettingsBuilder settings)
        {
            return settings;
        }

        /// <summary>
        /// 添加常规序列化，默认采用UTF-8编码
        /// </summary>
        /// <param name="jsSettings">配置（可选）</param>
        /// <param name="encoding">编码格式（可选）</param>
        public static SerializerSettingsBuilder AddJsonNormalSerializer(this SerializerSettingsBuilder settings, JsonSerializerSettings jsSettings = null, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;

            settings.AddProvider(PROVIDER_GROUP + JsonConvertProvider.PROVIDER_NAME, () =>
            {
                return new JsonConvertProvider(encoding, jsSettings);
            });
            return settings;
        }

        /// <summary>
        /// 添加压缩Json序列化，默认采用UTF-8编码
        /// </summary>
        /// <param name="jsSettings">配置（可选）</param>
        /// <param name="encoding">编码格式（可选）</param>
        public static SerializerSettingsBuilder AddJsonGzSerializer(this SerializerSettingsBuilder settings, JsonSerializerSettings jsSettings = null, Encoding encoding = null)
        {
            encoding = encoding ?? settings.GlobalEncoding;

            settings.AddProvider(PROVIDER_GROUP + GzJsonConvertProvider.PROVIDER_NAME, () =>
            {
                return new GzJsonConvertProvider(encoding, jsSettings);
            });
            return settings;
        }
    }
}
