using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public class CacheStorePipeline : ICacheStorePipeline
    {
        public EndPoint EndPoint { get; set; }
        public virtual bool IsConnected { get; }

        public virtual Task<byte[]> SendAsnyc(byte[] write) => throw new NotImplementedException();
        public virtual Task<bool> ConnectAsync() => throw new NotImplementedException();
    }
}
