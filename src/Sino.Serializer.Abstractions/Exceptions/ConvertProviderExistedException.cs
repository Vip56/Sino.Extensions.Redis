using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.Serializer.Abstractions.Exceptions
{
    /// <summary>
    /// 序列化已存在异常
    /// </summary>
    public class ConvertProviderExistedException : ArgumentException
    {
        public ConvertProviderExistedException()
            : base() { }

        public ConvertProviderExistedException(string message)
            : base(message) { }

    }
}
