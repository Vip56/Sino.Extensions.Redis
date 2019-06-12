using System;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using BeetleX.Clients;
using BeetleX;

namespace Sino.CacheStore.Handler
{
    public class RedisCacheStorePipeline : CacheStorePipeline
    {
        private TcpClient _connection;

        /// <summary>
        /// 是否连接
        /// </summary>
        public override bool IsConnected
        {
            get
            {
                return _connection.IsConnected;
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

        public RedisCacheStorePipeline(string host, int port)
        {
            EndPoint = new IPEndPoint(IPAddress.Parse(host), port);
            _connection = SocketFactory.CreateClient<TcpClient>(host, port);
        }

        /// <summary>
        /// 发起连接
        /// </summary>
        public override Task<bool> ConnectAsync()
        {
            if (!_connection.IsConnected)
            {
                _connection.Connect();
            }
            return Task.FromResult(_connection.IsConnected);
        }

        public override async Task<byte[]> SendAsnyc(byte[] write)
        {
            TcpClient wresult = _connection.SendMessage(write);

            while (true)
            {
                var rresult = _connection.Receive();
                var buffer = rresult.GetReadBuffers().Data;
                return buffer;
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
