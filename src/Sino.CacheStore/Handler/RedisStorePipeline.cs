using Pipelines.Sockets.Unofficial;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Buffers;
using System.Threading;

namespace Sino.CacheStore.Handler
{
    public class RedisStorePipeline : StorePipeline
    {
        private SocketConnection _connection;

        /// <summary>
        /// 是否连接
        /// </summary>
        public override bool IsConnected
        {
            get
            {
                return _connection.Socket.Connected;
            }
        }

        /// <summary>
        /// 重连尝试次数
        /// </summary>
        public int ReconnectAttempts { get; set; }

        /// <summary>
        /// 每次重连等待时间，单位毫秒
        /// </summary>
        public int ReconnectWait { get; set; }

        public RedisStorePipeline(string host, int port)
        {
            EndPoint = new IPEndPoint(IPAddress.Parse(host), port);
        }

        /// <summary>
        /// 发起连接
        /// </summary>
        public override async Task<bool> ConnectAsync()
        {
            _connection = await SocketConnection.ConnectAsync(EndPoint);

            return _connection.Socket.Connected;
        }

        public override async Task<byte[]> SendAsnyc(byte[] write)
        {
            var wresult = await _connection.Output.WriteAsync(write).ConfigureAwait(false);
            _connection.Output.Complete();

            while (true)
            {
                var rresult = await _connection.Input.ReadAsync().ConfigureAwait(false);
                var buffer = rresult.Buffer;
                if (rresult.IsCompleted)
                {
                    return rresult.Buffer.ToArray();
                }

                _connection.Input.AdvanceTo(buffer.Start, buffer.End);
            }
        }

        public async Task ReconnectAsync()
        {
            int attempts = 0;
            while(attempts++ < ReconnectAttempts || ReconnectAttempts == -1)
            {
                if (await ConnectAsync())
                    return;

                Thread.Sleep(TimeSpan.FromMilliseconds(ReconnectWait));
            }
        }

        public async Task ConnectIfNotConnectedAsync()
        {
            if (!IsConnected)
                await ConnectAsync();
        }

        public void ExpectConnectedAsync()
        {
            if (!IsConnected)
                throw new InvalidOperationException("Client is not connected");
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
