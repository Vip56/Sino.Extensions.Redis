using Sino.Extensions.Redis.Commands;
using Sino.Extensions.Redis.Internal;
using Sino.Extensions.Redis.Internal.IO;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis
{
    public partial class RedisClient
    {
        const int DEFAULT_PORT = 6379;
        const bool DEFAULT_SSL = false;
        const int DEFAULT_CONCURRENCY = 1000;
        const int DEFAULT_BUFFERSIZE = 10240;
        readonly RedisConnector _connector;

        /// <summary>
        /// Occurs when the connection has sucessfully reconnected
        /// </summary>
        public event EventHandler Connected;

        public string Host { get { return GetHost(); } }

        public int Port { get { return GetPort(); } }

        public bool IsConnected { get { return _connector.IsConnected; } }

        public Encoding Encoding
        {
            get { return _connector.Encoding; }
            set { _connector.Encoding = value; }
        }

        public int ReceiveTimeout
        {
            get { return _connector.ReceiveTimeout; }
            set { _connector.ReceiveTimeout = value; }
        }

        public int SendTimeout
        {
            get { return _connector.SendTimeout; }
            set { _connector.SendTimeout = value; }
        }

        public int ReconnectAttempts
        {
            get { return _connector.ReconnectAttempts; }
            set { _connector.ReconnectAttempts = value; }
        }

        public int ReconnectWait
        {
            get { return _connector.ReconnectWait; }
            set { _connector.ReconnectWait = value; }
        }

        public RedisClient(string host)
            : this(host, DEFAULT_PORT) { }

        public RedisClient(string host, int port)
            : this(host, port, DEFAULT_SSL) { }

        public RedisClient(string host, int port, bool ssl)
            : this(host, port, ssl, DEFAULT_CONCURRENCY, DEFAULT_BUFFERSIZE) { }

        public RedisClient(EndPoint endpoint)
            : this(endpoint, DEFAULT_SSL) { }

        public RedisClient(EndPoint endpoint, bool ssl)
            : this(endpoint, ssl, DEFAULT_CONCURRENCY, DEFAULT_BUFFERSIZE) { }

        public RedisClient(string host, int port, int asyncConcurrency, int asyncBufferSize)
            : this(host, port, DEFAULT_SSL, asyncConcurrency, asyncBufferSize) { }

        public RedisClient(string host, int port, bool ssl, int asyncConcurrency, int asyncBufferSize)
            : this(new DnsEndPoint(host, port), ssl, asyncConcurrency, asyncBufferSize) { }

        public RedisClient(EndPoint endpoint, int asyncConcurrency, int asyncBufferSize)
            : this(endpoint, DEFAULT_SSL, asyncConcurrency, asyncBufferSize) { }

        public RedisClient(EndPoint endpoint, bool ssl, int asyncConcurrency, int asyncBufferSize)
            : this(new RedisSocket(ssl), endpoint, asyncConcurrency, asyncBufferSize) { }

        public RedisClient(IRedisSocket socket, EndPoint endpoint)
            : this(socket, endpoint, DEFAULT_CONCURRENCY, DEFAULT_BUFFERSIZE) { }

        public RedisClient(IRedisSocket socket, EndPoint endpoint, int asyncConcurrency, int asyncBufferSize)
        {
            _connector = new RedisConnector(endpoint, socket, asyncConcurrency, asyncBufferSize);

            _connector.Connected += OnConnectionConnected;
        }

        void OnConnectionConnected(object sender, EventArgs args)
        {
            Connected?.Invoke(this, args);
        }

        string GetHost()
        {
            if (_connector.EndPoint is IPEndPoint)
                return (_connector.EndPoint as IPEndPoint).Address.ToString();
            else if (_connector.EndPoint is DnsEndPoint)
                return (_connector.EndPoint as DnsEndPoint).Host;
            else
                return null;
        }

        int GetPort()
        {
            if (_connector.EndPoint is IPEndPoint)
                return (_connector.EndPoint as IPEndPoint).Port;
            else if (_connector.EndPoint is DnsEndPoint)
                return (_connector.EndPoint as DnsEndPoint).Port;
            else
                return -1;
        }

        public bool Connect(int timeout)
        {
            return _connector.Connect();
        }

        public Task<bool> ConnectAsync()
        {
            return _connector.ConnectAsync();
        }

        public T Write<T>(RedisCommand<T> command)
        {
            return _connector.Call(command);
        }

        public Task<T> WriteAsync<T>(RedisCommand<T> command)
        {
            return _connector.CallAsync(command);
        }

        /// <summary>
        /// 利用密码进行授权
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>状态消息</returns>
        public string Auth(string password)
        {
            return Write(ConnectionCommands.Auth(password));
        }

        public void Dispose()
        {
            _connector.Dispose();
        }
    }
}
