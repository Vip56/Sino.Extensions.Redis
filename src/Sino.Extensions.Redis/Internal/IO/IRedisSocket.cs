using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis.Internal.IO
{
    /// <summary>
    /// 套接字接口
    /// </summary>
    public interface IRedisSocket : IDisposable
    {
        /// <summary>
        /// 是否连接
        /// </summary>
        bool Connected { get; }

        /// <summary>
        /// 接收超时，单位毫秒
        /// </summary>
        int ReceiveTimeout { get; set; }

        /// <summary>
        /// 发送超时，单位毫秒
        /// </summary>
        int SendTimeout { get; set; }

        /// <summary>
        /// 发起连接
        /// </summary>
        void Connect(EndPoint endpoint);

        /// <summary>
        /// 发起连接
        /// </summary>
        bool ConnectAsync(SocketAsyncEventArgs args);

        /// <summary>
        /// 发送数据
        /// </summary>
        bool SendAsync(SocketAsyncEventArgs args);

        /// <summary>
        /// 接收数据
        /// </summary>
        Task<Stream> GetStream();
    }
}
