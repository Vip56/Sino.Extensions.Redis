using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class RedisBinaryWriter
    {
        const char Bulk = (char)RedisMessage.Bulk;
        const char MultiBulk = (char)RedisMessage.MultiBulk;
        const string BOL = "\r\n";

        readonly Encoding _encoding;

        public RedisBinaryWriter(Encoding encoding)
        {
            _encoding = encoding;
        }

        public byte[] Prepare(CacheStoreCommand command)
        {
            var parts = command.Command.Split(' ');
            var length = parts.Length + command.Arguments.Length;

            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms, _encoding))
                {
                    sw.Write(MultiBulk);
                    sw.Write(length);
                    sw.Write(BOL);

                    foreach (var part in parts)
                    {
                        sw.Write(Bulk);
                        sw.Write(_encoding.GetByteCount(part));
                        sw.Write(BOL);
                        sw.Write(part);
                        sw.Write(BOL);
                    }

                    foreach (var arg in command.Arguments)
                    {
                        sw.Write(Bulk);
                        if (arg is byte[] bytes)
                        {
                            sw.Write(bytes.Length);
                            sw.Write(BOL);
                            sw.Write(bytes);
                            sw.Write(BOL);
                        }
                        else
                        {
                            var str = string.Format(CultureInfo.InvariantCulture, "{0}", arg);
                            sw.Write(Bulk);
                            sw.Write(_encoding.GetByteCount(str));
                            sw.Write(BOL);
                            sw.Write(str);
                            sw.Write(BOL);
                        }
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}
