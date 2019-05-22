using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sino.CacheStore.Handler
{
    public interface IStorePipeline
    {
        EndPoint EndPoint { get; set; }
        bool IsConnected { get; }

        Task<byte[]> SendAsnyc(byte[] write);
        Task<bool> ConnectAsync();
    }
}
