using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis.Internal.IO
{
    public interface IRedisSocket : IDisposable
    {
        bool Connected { get; }

        int ReceiveTimeout { get; set; }

        int SendTimeout { get; set; }

        void Connect(EndPoint endpoint);

        bool ConnectAsync(SocketAsyncEventArgs args);

        bool SendAsync(SocketAsyncEventArgs args);

        Task<Stream> GetStream();
    }
}
