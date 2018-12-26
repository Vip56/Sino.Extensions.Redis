using Sino.Extensions.Redis.Commands;
using Sino.Extensions.Redis.Internal;
using Sino.Extensions.Redis.Internal.IO;
using System;
using System.IO;
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
        readonly RedisTransaction _transaction;
        readonly SubscriptionListener _subscription;
        readonly MonitorListener _monitor;
        bool _streaming;

        /// <summary>
        /// Occurs when a subscription message is received
        /// </summary>
        public event EventHandler<RedisSubscriptionReceivedEventArgs> SubscriptionReceived;

        /// <summary>
        /// Occurs when a subscription channel is added or removed
        /// </summary>
        public event EventHandler<RedisSubscriptionChangedEventArgs> SubscriptionChanged;

        public event EventHandler<RedisTransactionQueuedEventArgs> TransactionQueued;

        /// <summary>
        /// Occurs when a monitor message is received
        /// </summary>
        public event EventHandler<RedisMonitorEventArgs> MonitorReceived;

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
            _transaction = new RedisTransaction(_connector);
            _subscription = new SubscriptionListener(_connector);
            _monitor = new MonitorListener(_connector);

            _subscription.MessageReceived += OnSubscriptionReceived;
            _subscription.Changed += OnSubscriptionChanged;
            _monitor.MonitorReceived += OnMonitorReceived;
            _connector.Connected += OnConnectionConnected;
            _transaction.TransactionQueued += OnTransactionQueued;
        }

        /// <summary>
        /// Begin buffered pipeline mode (calls return immediately; use EndPipe() to execute batch)
        /// </summary>
        public void StartPipe()
        {
            _connector.BeginPipe();
        }

        /// <summary>
        /// Begin buffered pipeline mode within the context of a transaction (calls return immediately; use EndPipe() to excute batch)
        /// </summary>
        public void StartPipeTransaction()
        {
            _connector.BeginPipe();
            Multi();
        }

        /// <summary>
        /// Execute pipeline commands
        /// </summary>
        /// <returns>Array of batched command results</returns>
        public object[] EndPipe()
        {
            if (_transaction.Active)
                return _transaction.Execute();
            else
                return _connector.EndPipe();
        }

        /// <summary>
        /// Stream a BULK reply from the server using default buffer size
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="destination">Destination stream</param>
        /// <param name="func">Client command to execute (BULK reply only)</param>
        public void StreamTo<T>(Stream destination, Func<IRedisClientSync, T> func)
        {
            StreamTo(destination, DEFAULT_BUFFERSIZE, func);
        }

        /// <summary>
        /// Stream a BULK reply from the server
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="destination">Destination stream</param>
        /// <param name="bufferSize">Size of buffer used to write server response</param>
        /// <param name="func">Client command to execute (BULK reply only)</param>
        public void StreamTo<T>(Stream destination, int bufferSize, Func<IRedisClientSync, T> func)
        {
            _streaming = true;
            func(this);
            _streaming = false;
            _connector.Read(destination, bufferSize);
        }

        void OnMonitorReceived(object sender, RedisMonitorEventArgs obj)
        {
            MonitorReceived?.Invoke(this, obj);
        }

        void OnSubscriptionReceived(object sender, RedisSubscriptionReceivedEventArgs args)
        {
            SubscriptionReceived?.Invoke(this, args);
        }

        void OnSubscriptionChanged(object sender, RedisSubscriptionChangedEventArgs args)
        {
            SubscriptionChanged?.Invoke(this, args);
        }

        void OnConnectionConnected(object sender, EventArgs args)
        {
            Connected?.Invoke(this, args);
        }

        void OnTransactionQueued(object sender, RedisTransactionQueuedEventArgs args)
        {
            TransactionQueued?.Invoke(this, args);
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
            if (_transaction.Active)
                return _transaction.Write(command);
            else if (_monitor.Listening)
                return default(T);
            else if (_streaming)
            {
                _connector.Write(command);
                return default(T);
            }
            else
                return _connector.Call(command);
        }

        public Task<T> WriteAsync<T>(RedisCommand<T> command)
        {
            if (_transaction.Active)
                return _transaction.WriteAsync(command);
            else
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
