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
    /// 返回类型为字节数组的命令
    /// </summary>
    public class BytesCommand : CacheStoreCommand<byte[]>
    {
        public BytesCommand(string command, params object[] args)
            : base(command, args) { }
    }
}
