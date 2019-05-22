using Sino.CacheStore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public class RedisStoreHandler : StoreHandler
    {
        readonly Encoding _encoding;

        public IStorePipeline Pipeline { get; set; }

        public RedisStoreHandler(Encoding encoding)
        {
            _encoding = encoding;
        }

        public override async Task<T> Process<T>(CacheStoreCommand<T> command)
        {
            var rbw = new RedisBinaryWriter(_encoding);
            var writeBytes = rbw.Prepare(command);

            var readBytes = await Pipeline.SendAsnyc(writeBytes);

            using (var ms = new MemoryStream(readBytes))
            {

            }
        }
    }
}
