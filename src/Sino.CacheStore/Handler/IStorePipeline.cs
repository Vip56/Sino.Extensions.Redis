using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public interface IStorePipeline
    {
        Task<byte[]> SendAsnyc(byte[] write);
    }
}
