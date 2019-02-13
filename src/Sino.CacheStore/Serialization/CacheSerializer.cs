using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Serializations
{
    /// <summary>
    /// 序列化抽象类
    /// </summary>
    public abstract class CacheSerializer : ICacheSerializer
    {
        public abstract byte[] Serialize<T>(T value) where T: class;

        public abstract T Deserialize<T>(byte[] data) where T: class;
    }
}
