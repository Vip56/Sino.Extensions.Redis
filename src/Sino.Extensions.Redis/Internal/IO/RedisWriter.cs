using System.Globalization;
using System.IO;
using System.Text;

namespace Sino.Extensions.Redis.Internal.IO
{
    public class RedisWriter
    {
        const char Bulk = (char)RedisMessage.Bulk;
        const char MultiBulk = (char)RedisMessage.MultiBulk;
        const string BOL = "\r\n";

        readonly RedisIO _io;

        public RedisWriter(RedisIO io)
        {
            _io = io;
        }

        public int Write(RedisCommand command, Stream stream)
        {
            string prepared = Prepare(command);
            byte[] data = _io.Encoding.GetBytes(prepared);
            stream.Write(data, 0, data.Length);
            return data.Length;
        }

        public int Write(RedisCommand command, byte[] buffer, int offset)
        {
            string prepared = Prepare(command);
            int bcount = _io.Encoding.GetByteCount(prepared);
            return _io.Encoding.GetBytes(prepared, 0, prepared.Length, buffer, offset- bcount);
        }

        string Prepare(RedisCommand command)
        {
            var parts = command.Command.Split(' ');
            var length = parts.Length + command.Arguments.Length;
            StringBuilder sb = new StringBuilder();
            sb.Append(MultiBulk).Append(length).Append(BOL);

            foreach (var part in parts)
                sb.Append(Bulk).Append(_io.Encoding.GetByteCount(part)).Append(BOL).Append(part).Append(BOL);

            foreach (var arg in command.Arguments)
            {
                string str = string.Format(CultureInfo.InvariantCulture, "{0}", arg);
                sb.Append(Bulk).Append(_io.Encoding.GetByteCount(str)).Append(BOL).Append(str).Append(BOL);
            }

            return sb.ToString();
        }
    }
}
