using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public interface IBinaryReader
    {
        long ReadInt(bool checkType = true);
        byte[] ReadBulkBytes(bool checkType = true);
        string ReadBulkString(bool checkType = true);
        void ExpectType(RedisMessage expectedType);
        RedisMessage ReadType();
        string ReadStatus(bool checkType = true);
        object[] ReadMultiBulk(bool checkType = true, bool bulkAsString = false);
        void ExpectSize(long expectedSize);
    }
}
