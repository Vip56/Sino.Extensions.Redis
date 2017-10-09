using System;

namespace Sino.Extensions.Redis
{
    /// <summary>
    /// Redis角色信息基础类
    /// </summary>
    public abstract class RedisRole
    {
        readonly string _roleName;

        /// <summary>
        /// 获取角色类型
        /// </summary>
        public string RoleName { get { return _roleName; } }

        internal RedisRole(string roleName)
        {
            _roleName = roleName;
        }
    }

    /// <summary>
    /// 获取主Redis信息
    /// </summary>
    public class RedisMasterRole : RedisRole
    {
        readonly long _replicationOffset;
        readonly Tuple<string, int, int>[] _slaves;

        /// <summary>
        /// Get the master replication offset
        /// </summary>
        public long ReplicationOffset { get { return _replicationOffset; } }

        /// <summary>
        /// Get the slaves associated with the current master
        /// </summary>
        public Tuple<string, int, int>[] Slaves { get { return _slaves; } }

        internal RedisMasterRole(string role, long replicationOffset, Tuple<string, int, int>[] slaves)
            : base(role)
        {
            _replicationOffset = replicationOffset;
            _slaves = slaves;
        }
    }

    /// <summary>
    /// Represents information on the Redis slave role
    /// </summary>
    public class RedisSlaveRole : RedisRole
    {
        readonly string _masterIp;
        readonly int _masterPort;
        readonly string _replicationState;
        readonly long _dataReceived;

        /// <summary>
        /// Get the IP address of the master node
        /// </summary>
        public string MasterIp { get { return _masterIp; } }

        /// <summary>
        /// Get the port of the master node
        /// </summary>
        public int MasterPort { get { return _masterPort; } }

        /// <summary>
        /// Get the replication state
        /// </summary>
        public string ReplicationState { get { return _replicationState; } }

        /// <summary>
        /// Get the number of bytes received
        /// </summary>
        public long DataReceived { get { return _dataReceived; } }

        internal RedisSlaveRole(string role, string masterIp, int masterPort, string replicationState, long dataReceived)
            : base(role)
        {
            _masterIp = masterIp;
            _masterPort = masterPort;
            _replicationState = replicationState;
            _dataReceived = dataReceived;
        }
    }

    /// <summary>
    /// Represents information on the Redis sentinel role
    /// </summary>
    public class RedisSentinelRole : RedisRole
    {
        readonly string[] _masters;

        /// <summary>
        /// Get the masters known to the current Sentinel
        /// </summary>
        public string[] Masters { get { return _masters; } }

        internal RedisSentinelRole(string role, string[] masters)
            : base(role)
        {
            _masters = masters;
        }
    }
}
