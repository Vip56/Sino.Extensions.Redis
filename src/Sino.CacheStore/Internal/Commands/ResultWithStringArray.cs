using System;
using System.Collections.Generic;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class ResultWithStringArray : CacheStoreCommand<string[]>
    {
        readonly ResultWithString _memberCommand;

        public ResultWithStringArray(string command, params object[] args)
            : base(command, args)
        {
            _memberCommand = new ResultWithString(command, args);
        }

        public override string[] Parse(IBinaryReader reader)
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
