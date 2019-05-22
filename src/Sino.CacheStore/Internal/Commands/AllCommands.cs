using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    /// <summary>
    /// 返回类型为Bool的命令
    /// </summary>
    public class BoolCommand : CacheStoreCommand<bool>
    {
        public BoolCommand(string command, params object[] args)
            : base(command, args) { }
    }

    /// <summary>
    /// 返回类型为Int的命令
    /// </summary>
    public class IntCommand : CacheStoreCommand<long>
    {
        public IntCommand(string command, params object[] args)
            : base(command, args) { }
    }

    /// <summary>
    /// 返回类型为状态的命令
    /// </summary>
    public class StatusCommand : CacheStoreCommand<string>
    {
        public bool IsEmpty { get; set; } = false;

        public bool IsNullable { get; set; } = false;

        public StatusCommand(string command, params object[] args)
            : base(command, args) { }
    }

    /// <summary>
    /// 返回类型为字符串的命令
    /// </summary>
    public class StringCommand : CacheStoreCommand<string>
    {
        public bool IsNullable { get; set; } = false;

        public StringCommand(string command, params object[] args)
            : base(command, args) { }
    }

    /// <summary>
    /// 返回类型为元组的命令
    /// </summary>
    public class TupleCommand : CacheStoreCommand<Tuple<string, string>>
    {
        public TupleCommand(string command, params object[] args)
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
}
