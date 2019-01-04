using System;
using System.Collections.Generic;

namespace Sino.Extensions.Redis
{
    public partial class RedisClient
    {
        #region Sets

        /// <summary>
        /// Add one or more members to a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="members">Members to add to set</param>
        /// <returns>Number of elements added to set</returns>
        public long SAdd(string key, params object[] members)
        {
            return Write(RedisCommands.SAdd(key, members));
        }

        /// <summary>
        /// Get the number of members in a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <returns>Number of elements in set</returns>
        public long SCard(string key)
        {
            return Write(RedisCommands.SCard(key));
        }

        /// <summary>
        /// Subtract multiple sets
        /// </summary>
        /// <param name="keys">Set keys to subtract</param>
        /// <returns>Array of elements in resulting set</returns>
        public string[] SDiff(params string[] keys)
        {
            return Write(RedisCommands.SDiff(keys));
        }

        /// <summary>
        /// Subtract multiple sets and store the resulting set in a key
        /// </summary>
        /// <param name="destination">Destination key</param>
        /// <param name="keys">Set keys to subtract</param>
        /// <returns>Number of elements in the resulting set</returns>
        public long SDiffStore(string destination, params string[] keys)
        {
            return Write(RedisCommands.SDiffStore(destination, keys));
        }

        /// <summary>
        /// Intersect multiple sets
        /// </summary>
        /// <param name="keys">Set keys to intersect</param>
        /// <returns>Array of elements in resulting set</returns>
        public string[] SInter(params string[] keys)
        {
            return Write(RedisCommands.SInter(keys));
        }

        /// <summary>
        /// Intersect multiple sets and store the resulting set in a key
        /// </summary>
        /// <param name="destination">Destination key</param>
        /// <param name="keys">Set keys to intersect</param>
        /// <returns>Number of elements in resulting set</returns>
        public long SInterStore(string destination, params string[] keys)
        {
            return Write(RedisCommands.SInterStore(destination, keys));
        }

        /// <summary>
        /// Determine if a given value is a member of a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="member">Member to lookup</param>
        /// <returns>True if member exists in set</returns>
        public bool SIsMember(string key, object member)
        {
            return Write(RedisCommands.SIsMember(key, member));
        }

        /// <summary>
        /// Get all the members in a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <returns>All elements in the set</returns>
        public string[] SMembers(string key)
        {
            return Write(RedisCommands.SMembers(key));
        }

        /// <summary>
        /// Move a member from one set to another
        /// </summary>
        /// <param name="source">Source key</param>
        /// <param name="destination">Destination key</param>
        /// <param name="member">Member to move</param>
        /// <returns>True if element was moved</returns>
        public bool SMove(string source, string destination, object member)
        {
            return Write(RedisCommands.SMove(source, destination, member));
        }

        /// <summary>
        /// Remove and return a random member from a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <returns>The removed element</returns>
        public string SPop(string key)
        {
            return Write(RedisCommands.SPop(key));
        }

        /// <summary>
        /// Get a random member from a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <returns>One random element from set</returns>
        public string SRandMember(string key)
        {
            return Write(RedisCommands.SRandMember(key));
        }

        /// <summary>
        /// Get one or more random members from a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="count">Number of elements to return</param>
        /// <returns>One or more random elements from set</returns>
        public string[] SRandMember(string key, long count)
        {
            return Write(RedisCommands.SRandMember(key, count));
        }

        /// <summary>
        /// Remove one or more members from a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="members">Set members to remove</param>
        /// <returns>Number of elements removed from set</returns>
        public long SRem(string key, params object[] members)
        {
            return Write(RedisCommands.SRem(key, members));
        }

        /// <summary>
        /// Add multiple sets
        /// </summary>
        /// <param name="keys">Set keys to union</param>
        /// <returns>Array of elements in resulting set</returns>
        public string[] SUnion(params string[] keys)
        {
            return Write(RedisCommands.SUnion(keys));
        }

        /// <summary>
        /// Add multiple sets and store the resulting set in a key
        /// </summary>
        /// <param name="destination">Destination key</param>
        /// <param name="keys">Set keys to union</param>
        /// <returns>Number of elements in resulting set</returns>
        public long SUnionStore(string destination, params string[] keys)
        {
            return Write(RedisCommands.SUnionStore(destination, keys));
        }

        /// <summary>
        /// Iterate the elements of a set field
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="cursor">The cursor returned by the server in the previous call, or 0 if this is the first call</param>
        /// <param name="pattern">Glob-style pattern to filter returned elements</param>
        /// <param name="count">Maximum number of elements to return</param>
        /// <returns>Updated cursor and result set</returns>
        public RedisScan<string> SScan(string key, long cursor, string pattern = null, long? count = null)
        {
            return Write(RedisCommands.SScan(key, cursor, pattern, count));
        }

        #endregion

        #region Strings

        /// <summary>
        /// Append a value to a key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to append to key</param>
        /// <returns>Length of string after append</returns>
        public long Append(string key, object value)
        {
            return Write(RedisCommands.Append(key, value));
        }

        /// <summary>
        /// Count set bits in a string
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <param name="start">Start offset</param>
        /// <param name="end">Stop offset</param>
        /// <returns>Number of bits set to 1</returns>
        public long BitCount(string key, long? start = null, long? end = null)
        {
            return Write(RedisCommands.BitCount(key, start, end));
        }

        /// <summary>
        /// Perform bitwise operations between strings
        /// </summary>
        /// <param name="operation">Bit command to execute</param>
        /// <param name="destKey">Store result in destination key</param>
        /// <param name="keys">Keys to operate</param>
        /// <returns>Size of string stored in the destination key</returns>
        public long BitOp(RedisBitOp operation, string destKey, params string[] keys)
        {
            return Write(RedisCommands.BitOp(operation, destKey, keys));
        }

        /// <summary>
        /// Find first bit set or clear in a string
        /// </summary>
        /// <param name="key">Key to examine</param>
        /// <param name="bit">Bit value (1 or 0)</param>
        /// <param name="start">Examine string at specified byte offset</param>
        /// <param name="end">Examine string to specified byte offset</param>
        /// <returns>Position of the first bit set to the specified value</returns>
        public long BitPos(string key, bool bit, long? start = null, long? end = null)
        {
            return Write(RedisCommands.BitPos(key, bit, start, end));
        }

        /// <summary>
        /// Decrement the integer value of a key by one
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <returns>Value of key after decrement</returns>
        public long Decr(string key)
        {
            return Write(RedisCommands.Decr(key));
        }

        /// <summary>
        /// Decrement the integer value of a key by the given number
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="decrement">Decrement value</param>
        /// <returns>Value of key after decrement</returns>
        public long DecrBy(string key, long decrement)
        {
            return Write(RedisCommands.DecrBy(key, decrement));
        }

        /// <summary>
        /// Get the value of a key
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <returns>Value of key</returns>
        public string Get(string key)
        {
            return Write(RedisCommands.Get(key));
        }

        /// <summary>
        /// Returns the bit value at offset in the string value stored at key
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <param name="offset">Offset of key to check</param>
        /// <returns>Bit value stored at offset</returns>
        public bool GetBit(string key, uint offset)
        {
            return Write(RedisCommands.GetBit(key, offset));
        }

        /// <summary>
        /// Get a substring of the string stored at a key
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <param name="start">Start offset</param>
        /// <param name="end">End offset</param>
        /// <returns>Substring in the specified range</returns>
        public string GetRange(string key, long start, long end)
        {
            return Write(RedisCommands.GetRange(key, start, end));
        }

        /// <summary>
        /// Set the string value of a key and return its old value
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <returns>Old value stored at key, or null if key did not exist</returns>
        public string GetSet(string key, object value)
        {
            return Write(RedisCommands.GetSet(key, value));
        }

        /// <summary>
        /// Increment the integer value of a key by one
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <returns>Value of key after increment</returns>
        public long Incr(string key)
        {
            return Write(RedisCommands.Incr(key));
        }

        /// <summary>
        /// Increment the integer value of a key by the given amount
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="increment">Increment amount</param>
        /// <returns>Value of key after increment</returns>
        public long IncrBy(string key, long increment)
        {
            return Write(RedisCommands.IncrBy(key, increment));
        }

        /// <summary>
        /// Increment the float value of a key by the given amount
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="increment">Increment amount</param>
        /// <returns>Value of key after increment</returns>
        public double IncrByFloat(string key, double increment)
        {
            return Write(RedisCommands.IncrByFloat(key, increment));
        }

        /// <summary>
        /// Get the values of all the given keys
        /// </summary>
        /// <param name="keys">Keys to lookup</param>
        /// <returns>Array of values at the specified keys</returns>
        public string[] MGet(params string[] keys)
        {
            return Write(RedisCommands.MGet(keys));
        }

        /// <summary>
        /// Set multiple keys to multiple values
        /// </summary>
        /// <param name="keyValues">Key values to set</param>
        /// <returns>Status code</returns>
        public string MSet(params Tuple<string, string>[] keyValues)
        {
            return Write(RedisCommands.MSet(keyValues));
        }

        /// <summary>
        /// Set multiple keys to multiple values
        /// </summary>
        /// <param name="keyValues">Key values to set [k1, v1, k2, v2, ..]</param>
        /// <returns>Status code</returns>
        public string MSet(params string[] keyValues)
        {
            return Write(RedisCommands.MSet(keyValues));
        }

        /// <summary>
        /// Set multiple keys to multiple values, only if none of the keys exist
        /// </summary>
        /// <param name="keyValues">Key values to set</param>
        /// <returns>True if all keys were set</returns>
        public bool MSetNx(params Tuple<string, string>[] keyValues)
        {
            return Write(RedisCommands.MSetNx(keyValues));
        }

        /// <summary>
        /// Set multiple keys to multiple values, only if none of the keys exist
        /// </summary>
        /// <param name="keyValues">Key values to set [k1, v1, k2, v2, ..]</param>
        /// <returns>True if all keys were set</returns>
        public bool MSetNx(params string[] keyValues)
        {
            return Write(RedisCommands.MSetNx(keyValues));
        }

        /// <summary>
        /// Set the value and expiration in milliseconds of a key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="milliseconds">Expiration in milliseconds</param>
        /// <param name="value">Value to set</param>
        /// <returns>Status code</returns>
        public string PSetEx(string key, long milliseconds, object value)
        {
            return Write(RedisCommands.PSetEx(key, milliseconds, value));
        }

        /// <summary>
        /// Set the string value of a key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <returns>Status code</returns>
        public string Set(string key, object value)
        {
            return Write(RedisCommands.Set(key, value));
        }

        /// <summary>
        /// Set the string value of a key with atomic expiration and existence condition
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <param name="expiration">Set expiration to nearest millisecond</param>
        /// <param name="condition">Set key if existence condition</param>
        /// <returns>Status code, or null if condition not met</returns>
        public string Set(string key, object value, TimeSpan expiration, RedisExistence? condition = null)
        {
            return Write(RedisCommands.Set(key, value, expiration, condition));
        }

        /// <summary>
        /// Set the string value of a key with atomic expiration and existence condition
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <param name="expirationSeconds">Set expiration to nearest second</param>
        /// <param name="condition">Set key if existence condition</param>
        /// <returns>Status code, or null if condition not met</returns>
        public string Set(string key, object value, int? expirationSeconds = null, RedisExistence? condition = null)
        {
            return Write(RedisCommands.Set(key, value, expirationSeconds, condition));
        }

        /// <summary>
        /// Set the string value of a key with atomic expiration and existence condition
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <param name="expirationMilliseconds">Set expiration to nearest millisecond</param>
        /// <param name="condition">Set key if existence condition</param>
        /// <returns>Status code, or null if condition not met</returns>
        public string Set(string key, object value, long? expirationMilliseconds = null, RedisExistence? condition = null)
        {
            return Write(RedisCommands.Set(key, value, expirationMilliseconds, condition));
        }

        /// <summary>
        /// Sets or clears the bit at offset in the string value stored at key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="offset">Modify key at offset</param>
        /// <param name="value">Value to set (on or off)</param>
        /// <returns>Original bit stored at offset</returns>
        public bool SetBit(string key, uint offset, bool value)
        {
            return Write(RedisCommands.SetBit(key, offset, value));
        }

        /// <summary>
        /// Set the value and expiration of a key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="seconds">Expiration in seconds</param>
        /// <param name="value">Value to set</param>
        /// <returns>Status code</returns>
        public string SetEx(string key, long seconds, object value)
        {
            return Write(RedisCommands.SetEx(key, seconds, value));
        }

        /// <summary>
        /// Set the value of a key, only if the key does not exist
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <returns>True if key was set</returns>
        public bool SetNx(string key, object value)
        {
            return Write(RedisCommands.SetNx(key, value));
        }

        /// <summary>
        /// Overwrite part of a string at key starting at the specified offset
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="offset">Start offset</param>
        /// <param name="value">Value to write at offset</param>
        /// <returns>Length of string after operation</returns>
        public long SetRange(string key, uint offset, object value)
        {
            return Write(RedisCommands.SetRange(key, offset, value));
        }

        /// <summary>
        /// Get the length of the value stored in a key
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <returns>Length of string at key</returns>
        public long StrLen(string key)
        {
            return Write(RedisCommands.StrLen(key));
        }

        #endregion

        #region Server

        /// <summary>
        /// Asyncronously rewrite the append-only file
        /// </summary>
        /// <returns>Status code</returns>
        public string BgRewriteAof()
        {
            return Write(RedisCommands.BgRewriteAof());
        }

        /// <summary>
        /// Asynchronously save the dataset to disk
        /// </summary>
        /// <returns>Status code</returns>
        public string BgSave()
        {
            return Write(RedisCommands.BgSave());
        }

        /// <summary>
        /// Kill the connection of a client
        /// </summary>
        /// <param name="ip">Client IP returned from CLIENT LIST</param>
        /// <param name="port">Client port returned from CLIENT LIST</param>
        /// <returns>Status code</returns>
        public string ClientKill(string ip, int port)
        {
            return Write(RedisCommands.ClientKill(ip, port));
        }

        /// <summary>
        /// Kill the connection of a client
        /// </summary>
        /// <param name="addr">client's ip:port</param>
        /// <param name="id">client's unique ID</param>
        /// <param name="type">client type (normal|slave|pubsub)</param>
        /// <param name="skipMe">do not kill the calling client</param>
        /// <returns>Nummber of clients killed</returns>
        public long ClientKill(string addr = null, string id = null, string type = null, bool? skipMe = null)
        {
            return Write(RedisCommands.ClientKill(addr, id, type, skipMe));
        }

        /// <summary>
        /// Get the list of client connections
        /// </summary>
        /// <returns>Formatted string of clients</returns>
        public string ClientList()
        {
            return Write(RedisCommands.ClientList());
        }

        /// <summary>
        /// Suspend all Redis clients for the specified amount of time
        /// </summary>
        /// <param name="milliseconds">Time to pause in milliseconds</param>
        /// <returns>Status code</returns>
        public string ClientPause(int milliseconds)
        {
            return Write(RedisCommands.ClientPause(milliseconds));
        }

        /// <summary>
        /// Suspend all Redis clients for the specified amount of time
        /// </summary>
        /// <param name="timeout">Time to pause</param>
        /// <returns>Status code</returns>
        public string ClientPause(TimeSpan timeout)
        {
            return Write(RedisCommands.ClientPause(timeout));
        }

        /// <summary>
        /// Get the current connection name
        /// </summary>
        /// <returns>Connection name</returns>
        public string ClientGetName()
        {
            return Write(RedisCommands.ClientGetName());
        }

        /// <summary>
        /// Set the current connection name
        /// </summary>
        /// <param name="connectionName">Name of connection (no spaces)</param>
        /// <returns>Status code</returns>
        public string ClientSetName(string connectionName)
        {
            return Write(RedisCommands.ClientSetName(connectionName));
        }

        /// <summary>
        /// Get the value of a configuration paramter
        /// </summary>
        /// <param name="parameter">Configuration parameter to lookup</param>
        /// <returns>Configuration value</returns>
        public Tuple<string, string>[] ConfigGet(string parameter)
        {
            return Write(RedisCommands.ConfigGet(parameter));
        }

        /// <summary>
        /// Reset the stats returned by INFO
        /// </summary>
        /// <returns>Status code</returns>
        public string ConfigResetStat()
        {
            return Write(RedisCommands.ConfigResetStat());
        }

        /// <summary>
        /// Rewrite the redis.conf file the server was started with, applying the minimal changes needed to make it reflect current configuration
        /// </summary>
        /// <returns>Status code</returns>
        public string ConfigRewrite()
        {
            return Write(RedisCommands.ConfigRewrite());
        }

        /// <summary>
        /// Set a configuration parameter to the given value
        /// </summary>
        /// <param name="parameter">Parameter to set</param>
        /// <param name="value">Value to set</param>
        /// <returns>Status code</returns>
        public string ConfigSet(string parameter, string value)
        {
            return Write(RedisCommands.ConfigSet(parameter, value));
        }

        /// <summary>
        /// Return the number of keys in the selected database
        /// </summary>
        /// <returns>Number of keys</returns>
        public long DbSize()
        {
            return Write(RedisCommands.DbSize());
        }

        /// <summary>
        /// Make the server crash :(
        /// </summary>
        /// <returns>Status code</returns>
        public string DebugSegFault()
        {
            return Write(RedisCommands.DebugSegFault());
        }

        /// <summary>
        /// Remove all keys from all databases
        /// </summary>
        /// <returns>Status code</returns>
        public string FlushAll()
        {
            return Write(RedisCommands.FlushAll());
        }

        /// <summary>
        /// Remove all keys from the current database
        /// </summary>
        /// <returns>Status code</returns>
        public string FlushDb()
        {
            return Write(RedisCommands.FlushDb());
        }

        /// <summary>
        /// Get information and statistics about the server
        /// </summary>
        /// <param name="section">all|default|server|clients|memory|persistence|stats|replication|cpu|commandstats|cluster|keyspace</param>
        /// <returns>Formatted string</returns>
        public string Info(string section = null)
        {
            return Write(RedisCommands.Info(section));
        }

        /// <summary>
        /// Get the timestamp of the last successful save to disk
        /// </summary>
        /// <returns>Date of last save</returns>
        public DateTime LastSave()
        {
            return Write(RedisCommands.LastSave());
        }

        /// <summary>
        /// Listen for all requests received by the server in real time
        /// </summary>
        /// <returns>Status code</returns>
        public string Monitor()
        {
            return _monitor.Start();
        }

        /// <summary>
        /// Get role information for the current Redis instance
        /// </summary>
        /// <returns>RedisMasterRole|RedisSlaveRole|RedisSentinelRole</returns>
        public RedisRole Role()
        {
            return Write(RedisCommands.Role());
        }

        /// <summary>
        /// Syncronously save the dataset to disk
        /// </summary>
        /// <returns>Status code</returns>
        public string Save()
        {
            return Write(RedisCommands.Save());
        }

        /// <summary>
        /// Syncronously save the dataset to disk an then shut down the server
        /// </summary>
        /// <param name="save">Force a DB saving operation even if no save points are configured</param>
        /// <returns>Status code</returns>
        public string Shutdown(bool? save = null)
        {
            return Write(RedisCommands.Shutdown(save));
        }

        /// <summary>
        /// Make the server a slave of another instance or promote it as master
        /// </summary>
        /// <param name="host">Master host</param>
        /// <param name="port">master port</param>
        /// <returns>Status code</returns>
        public string SlaveOf(string host, int port)
        {
            return Write(RedisCommands.SlaveOf(host, port));
        }

        /// <summary>
        /// Turn off replication, turning the Redis server into a master
        /// </summary>
        /// <returns>Status code</returns>
        public string SlaveOfNoOne()
        {
            return Write(RedisCommands.SlaveOfNoOne());
        }

        /// <summary>
        /// Get latest entries from the slow log
        /// </summary>
        /// <param name="count">Limit entries returned</param>
        /// <returns>Slow log entries</returns>
        public RedisSlowLogEntry[] SlowLogGet(long? count = null)
        {
            return Write(RedisCommands.SlowLogGet(count));
        }

        /// <summary>
        /// Get the length of the slow log
        /// </summary>
        /// <returns>Slow log length</returns>
        public long SlowLogLen()
        {
            return Write(RedisCommands.SlowLogLen());
        }

        /// <summary>
        /// Reset the slow log
        /// </summary>
        /// <returns>Status code</returns>
        public string SlowLogReset()
        {
            return Write(RedisCommands.SlowLogReset());
        }

        /// <summary>
        /// Internal command used for replication
        /// </summary>
        /// <returns>Byte array of Redis sync data</returns>
        public byte[] Sync()
        {
            return Write(RedisCommands.Sync());
        }

        /// <summary>
        /// Return the current server time
        /// </summary>
        /// <returns>Server time</returns>
        public DateTime Time()
        {
            return Write(RedisCommands.Time());
        }

        #endregion
    }
}
