using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    /// <summary>
    /// reader.ReadInt() == 1;
    /// </summary>
    public class BoolCommand : CacheStoreCommand<bool>
    {
        public BoolCommand(string command, params object[] args)
            : base(command, args) { }
    }

    /// <summary>
    /// reader.ReadBulkBytes(true);
    /// </summary>
    public class ResultWithBytes : CacheStoreCommand<byte[]>
    {
        public ResultWithBytes(string command, params object[] args)
            : base(command, args) { }
    }

    /// <summary>
    /// string result = reader.ReadBulkString();
    /// if (result == null)
    ///        return null;
    /// return double.Parse(result, NumberStyles.Float, CultureInfo.InvariantCulture);
    /// </summary>
    public class ResultWithFloat : CacheStoreCommand<double?>
    {
        public ResultWithFloat(string command, params object[] args)
            : base(command, args) { }
    }

    /// <summary>
    /// reader.ExpectType(RedisMessage.MultiBulk);
    /// long count = reader.ReadInt(false);
    /// var dict = new Dictionary<string, string>();
    /// string key = string.Empty;
    /// for (int i = 0; i < count; i++)
    /// {
    /// if (i % 2 == 0)
    /// key = reader.ReadBulkString();
    /// else
    /// dict[key] = reader.ReadBulkString();
    /// }
    /// return dict;
    /// </summary>
    public class ResultWithHash : CacheStoreCommand<Dictionary<string, string>>
    {
        public ResultWithHash(string command, params object[] args)
            : base(command, args) { }
    }

    /// <summary>
    /// reader.ReadInt();
    /// </summary>
    public class IntCommand : CacheStoreCommand<long>
    {
        public IntCommand(string command, params object[] args)
            : base(command, args) { }
    }

    public class StatusCommand : CacheStoreCommand<string>
    {
        public bool IsEmpty { get; set; } = false;

        public bool IsNullable { get; set; } = false;

        public StatusCommand(string command, params object[] args)
            : base(command, args) { }

        public string Parse(IBinaryReader reader)
        {
            if (IsEmpty)
            {
                RedisMessage type = reader.ReadType();
                if ((int)type == -1)
                    return string.Empty;
                else if (type == RedisMessage.Error)
                    throw new CacheStoreException(reader.ReadStatus(false));

                throw new CacheStoreProtocolException($"Unexpected type: {type}");
            }
            else if (IsNullable)
            {
                RedisMessage type = reader.ReadType();
                if (type == RedisMessage.Status)
                    return reader.ReadStatus(false);

                object[] result = reader.ReadMultiBulk(false);
                if (result != null)
                    throw new CacheStoreProtocolException($"Expecting null MULTI BULK response. Received: {result.ToString()}");

                return null;
            }
            else
                return reader.ReadStatus();
        }
    }

    public class StringCommand : CacheStoreCommand<string>
    {
        public bool IsNullable { get; set; } = false;

        public StringCommand(string command, params object[] args)
            : base(command, args) { }

        public string Parse(IBinaryReader reader)
        {
            if (IsNullable)
            {
                RedisMessage type = reader.ReadType();
                if (type == RedisMessage.Bulk)
                    return reader.ReadBulkString(false);
                reader.ReadMultiBulk(false);
                return null;
            }
            else
            {
                return reader.ReadBulkString();
            }
        }
    }

    public class ResultWithStringArray : CacheStoreCommand<string[]>
    {
        readonly StringCommand _memberCommand;

        public ResultWithStringArray(string command, params object[] args)
            : base(command, args)
        {
            _memberCommand = new StringCommand(command, args);
        }

        public string[] Parse(IBinaryReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            long count = reader.ReadInt(false);
            return Read(count, reader);
        }

        protected virtual string[] Read(long count, IBinaryReader reader)
        {
            string[] array = new string[count];
            for (int i = 0; i < array.Length; i++)
                array[i] = _memberCommand.Parse(reader);
            return array;
        }
    }

    public class TupleCommand : CacheStoreCommand<Tuple<string, string>>
    {
        public TupleCommand(string command, params object[] args)
            : base(command, args) { }

        public Tuple<string, string> Parse(IBinaryReader reader)
        {
            reader.ExpectType(RedisMessage.MultiBulk);
            reader.ExpectSize(2);
            return Tuple.Create(reader.ReadBulkString(), reader.ReadBulkString());
        }
    }
}
