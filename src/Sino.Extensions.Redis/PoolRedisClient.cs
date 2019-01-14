using Sino.Extensions.Redis.Commands;
using Sino.Extensions.Redis.Internal.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis
{
    public partial class PoolRedisClient : IDisposable
    {
        private EndPoint _endpoint;
        private string _password;
        private IRedisSocket _socket;

        private readonly ConcurrentQueue<RedisClient> _pool;
        private SemaphoreSlim _semaphore;

        public PoolRedisClient(string host, int port, int max = 100)
            : this(host, port, null, max) { }

        public PoolRedisClient(string host, int port, string password, int max = 100)
        {
            _endpoint = new IPEndPoint(IPAddress.Parse(host), port);
            _password = password;
            _pool = new ConcurrentQueue<RedisClient>();
            _semaphore = new SemaphoreSlim(max, max);
        }

        public PoolRedisClient(IRedisSocket socket, EndPoint endpoint, int max = 100)
        {
            _socket = socket;
            _endpoint = endpoint;
            _pool = new ConcurrentQueue<RedisClient>();
            _semaphore = new SemaphoreSlim(max, max);
        }

        public T Multi<T>(RedisCommand<T> cmd)
        {
            _semaphore.Wait(30000);

            RedisClient client = null;
            try
            {
                if (!_pool.TryDequeue(out client))
                {
                    if (_socket == null)
                    {
                        client = new RedisClient(_endpoint);
                    }
                    else
                    {
                        client = new RedisClient(_socket, _endpoint);
                    }
                    if (!string.IsNullOrEmpty(_password))
                        client.Auth(_password);
                }
                var result = client.Write(cmd);
                _pool.Enqueue(client);
                return result;
            }
            catch (SocketException)
            {
                if (client != null)
                    client.Dispose();
                throw;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public Task<T> MultiAsync<T>(RedisCommand<T> cmd)
        {
            _semaphore.Wait(30000);

            RedisClient client = null;
            try
            {
                if (!_pool.TryDequeue(out client))
                {
                    if (_socket == null)
                    {
                        client = new RedisClient(_endpoint);
                    }
                    else
                    {
                        client = new RedisClient(_socket, _endpoint);
                    }
                    if (!string.IsNullOrEmpty(_password))
                        client.Auth(_password);
                }
                var result = client.WriteAsync(cmd);
                _pool.Enqueue(client);
                return result;
            }
            catch (SocketException)
            {
                if (client != null)
                    client.Dispose();
                throw;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        #region Connection

        public string Echo(string message) => Multi(ConnectionCommands.Echo(message));

        public Task<string> EchoAsync(string message) => MultiAsync(ConnectionCommands.Echo(message));

        public string Ping() => Multi(ConnectionCommands.Ping());

        public Task<string> PingAsync() => MultiAsync(ConnectionCommands.Ping());

        public string Quit() => Multi(ConnectionCommands.Quit());

        public Task<string> QuitAsync() => MultiAsync(ConnectionCommands.Quit());

        public string Select(int index) => Multi(ConnectionCommands.Select(index));

        public Task<string> SelectAsync(int index) => MultiAsync(ConnectionCommands.Select(index));

        #endregion

        #region Keys

        public long Del(params string[] keys) => Multi(KeyCommands.Del(keys));

        public Task<long> DelAsync(params string[] keys) => MultiAsync(KeyCommands.Del(keys));

        public byte[] Dump(string key) => Multi(KeyCommands.Dump(key));

        public Task<byte[]> DumpAsync(string key) => MultiAsync(KeyCommands.Dump(key));

        public bool Exists(string key) => Multi(KeyCommands.Exists(key));

        public Task<bool> ExistsAsync(string key) => MultiAsync(KeyCommands.Exists(key));

        public bool Expire(string key, int seconds) => Multi(KeyCommands.Expire(key, seconds));

        public Task<bool> ExpireAsync(string key, int seconds) => MultiAsync(KeyCommands.Expire(key, seconds));

        public bool ExpireAt(string key, DateTime expirationDate) => Multi(KeyCommands.ExpireAt(key, expirationDate.GetUnixTime() / 1000));

        public Task<bool> ExpireAtAsync(string key, DateTime expirationDate) => MultiAsync(KeyCommands.ExpireAt(key, expirationDate.GetUnixTime() / 1000));

        public bool ExpireAt(string key, long timestamp) => Multi(KeyCommands.ExpireAt(key, timestamp));

        public Task<bool> ExpireAtAsync(string key, int timestamp) => MultiAsync(KeyCommands.ExpireAt(key, timestamp));

        public string[] Keys(string pattern) => Multi(KeyCommands.Keys(pattern));

        public Task<string[]> KeysAsync(string pattern) => MultiAsync(KeyCommands.Keys(pattern));

        public string Migrate(string host, int port, string key, int destinationDb, int timeoutMilliseconds) => Multi(KeyCommands.Migrate(host, port, key, destinationDb, timeoutMilliseconds));

        public Task<string> MigrateAsync(string host, int port, string key, int destinationDb, int timeoutMilliseconds) => MultiAsync(KeyCommands.Migrate(host, port, key, destinationDb, timeoutMilliseconds));

        public string Migrate(string host, int port, string key, int destinationDb, TimeSpan timeout) => Multi(KeyCommands.Migrate(host, port, key, destinationDb, (int)timeout.TotalMilliseconds));

        public Task<string> MigrateAsync(string host, int port, string key, int destinationDb, TimeSpan timeout) => MultiAsync(KeyCommands.Migrate(host, port, key, destinationDb, (int)timeout.TotalMilliseconds));

        public bool Move(string key, int database) => Multi(KeyCommands.Move(key, database));

        public Task<bool> MoveAsync(string key, int database) => MultiAsync(KeyCommands.Move(key, database));

        public long ObjectIdleTime(string key) => Multi(KeyCommands.ObjectIdleTime(key));

        public Task<long> ObjectIdleTimeAsync(string key) => MultiAsync(KeyCommands.ObjectIdleTime(key));

        public bool Persist(string key) => Multi(KeyCommands.Persist(key));

        public Task<bool> PersistAsync(string key) => MultiAsync(KeyCommands.Persist(key));

        public bool PExpire(string key, TimeSpan expiration) => Multi(KeyCommands.PExpire(key, (long)expiration.TotalMilliseconds));

        public Task<bool> PExpireAsync(string key, TimeSpan expiration) => MultiAsync(KeyCommands.PExpire(key, (long)expiration.TotalMilliseconds));

        public bool PExpire(string key, long milliseconds) => Multi(KeyCommands.PExpire(key, milliseconds));

        public Task<bool> PExpireAsync(string key, long milliseconds) => MultiAsync(KeyCommands.PExpire(key, milliseconds));

        public bool PExpireAt(string key, DateTime date) => Multi(KeyCommands.PExpireAt(key, date.GetUnixTime()));

        public Task<bool> PExpireAtAsync(string key, DateTime date) => MultiAsync(KeyCommands.PExpireAt(key, date.GetUnixTime()));

        public bool PExpireAt(string key, long timestamp) => Multi(KeyCommands.PExpireAt(key, timestamp));

        public Task<bool> PExpireAtAsync(string key, long timestamp) => MultiAsync(KeyCommands.PExpireAt(key, timestamp));

        public long PTtl(string key) => Multi(KeyCommands.PTtl(key));

        public Task<long> PTtlAsync(string key) => MultiAsync(KeyCommands.PTtl(key));

        public string RandomKey() => Multi(KeyCommands.RandomKey());

        public Task<string> RandomKeyAsync() => MultiAsync(KeyCommands.RandomKey());

        public string Rename(string key, string newKey) => Multi(KeyCommands.Rename(key, newKey));

        public Task<string> RenameAsync(string key, string newKey) => MultiAsync(KeyCommands.Rename(key, newKey));

        public bool RenameNx(string key, string newKey) => Multi(KeyCommands.RenameNx(key, newKey));

        public Task<bool> RenameNxAsync(string key, string newKey) => MultiAsync(KeyCommands.RenameNx(key, newKey));

        public string Restore(string key, long ttl, string serializedValue) => Multi(KeyCommands.Restore(key, ttl, serializedValue));

        public Task<string> RestoreAsync(string key, long ttl, string serializedValue) => MultiAsync(KeyCommands.Restore(key, ttl, serializedValue));

        public long Ttl(string key) => Multi(KeyCommands.Ttl(key));

        public Task<long> TtlAsync(string key) => MultiAsync(KeyCommands.Ttl(key));

        #endregion

        #region Hashes

        public long HDel(string key, params string[] fields) => Multi(HashCommands.HDel(key, fields));

        public Task<long> HDelAsync(string key, params string[] fields) => MultiAsync(HashCommands.HDel(key, fields));

        public bool HExists(string key, string field) => Multi(HashCommands.HExists(key, field));

        public Task<bool> HExistsAsync(string key, string field) => MultiAsync(HashCommands.HExists(key, field));

        public string HGet(string key, string field) => Multi(HashCommands.HGet(key, field));

        public Task<string> HGetAsync(string key, string field) => MultiAsync(HashCommands.HGet(key, field));

        public Dictionary<string, string> HGetAll(string key) => Multi(HashCommands.HGetAll(key));

        public Task<Dictionary<string, string>> HGetAllAsync(string key) => MultiAsync(HashCommands.HGetAll(key));

        public long HIncrBy(string key, string field, long increment) => Multi(HashCommands.HIncrBy(key, field, increment));

        public Task<long> HIncrByAsync(string key, string field, long increment) => MultiAsync(HashCommands.HIncrBy(key, field, increment));

        public double? HIncrByFloat(string key, string field, double increment) => Multi(HashCommands.HIncrByFloat(key, field, increment));

        public Task<double?> HIncrByFloatAsync(string key, string field, double increment) => MultiAsync(HashCommands.HIncrByFloat(key, field, increment));

        public string[] HKeys(string key) => Multi(HashCommands.HKeys(key));

        public Task<string[]> HKeysAsync(string key) => MultiAsync(HashCommands.HKeys(key));

        public long HLen(string key) => Multi(HashCommands.HLen(key));

        public Task<long> HLenAsync(string key) => MultiAsync(HashCommands.HLen(key));

        public string[] HMGet(string key, params string[] fields) => Multi(HashCommands.HMGet(key, fields));

        public string HMSet(string key, Dictionary<string, string> dict) => Multi(HashCommands.HMSet(key, dict));

        public Task<string> HMSetAsync(string key, Dictionary<string, string> dict) => MultiAsync(HashCommands.HMSet(key, dict));

        public bool HSet(string key, string field, object value) => Multi(HashCommands.HSet(key, field, value));

        public Task<bool> HSetAsync(string key, string field, object value) => MultiAsync(HashCommands.HSet(key, field, value));

        public bool HSetNx(string key, string field, object value) => Multi(HashCommands.HSetNx(key, field, value));

        public Task<bool> HSetNxAsync(string key, string field, object value) => MultiAsync(HashCommands.HSetNx(key, field, value));

        #endregion

        #region Lists

        public Tuple<string, string> BLPop(int timeout, params string[] keys) => Multi(ListCommands.BLPop(timeout, keys));

        public Task<Tuple<string, string>> BLPopAsync(int timeout, params string[] keys) => MultiAsync(ListCommands.BLPop(timeout, keys));

        public Tuple<string, string> BLPop(TimeSpan timeout, params string[] keys) => Multi(ListCommands.BLPop((int)timeout.TotalSeconds, keys));

        public Task<Tuple<string, string>> BLPopAsync(TimeSpan timeout, params string[] keys) => MultiAsync(ListCommands.BLPop((int)timeout.TotalSeconds, keys));

        public Tuple<string, string> BRPop(int timeout, params string[] keys) => Multi(ListCommands.BRPop(timeout, keys));

        public Task<Tuple<string, string>> BRPopAsync(int timeout, params string[] keys) => MultiAsync(ListCommands.BRPop(timeout, keys));

        public Tuple<string, string> BRPop(TimeSpan timeout, params string[] keys) => Multi(ListCommands.BRPop((int)timeout.TotalSeconds, keys));

        public Task<Tuple<string, string>> BRPopAsync(TimeSpan timeout, params string[] keys) => MultiAsync(ListCommands.BRPop((int)timeout.TotalSeconds, keys));

        public string BRPopLPush(string source, string destination, int timeout) => Multi(ListCommands.BRPopLPush(source, destination, timeout));

        public Task<string> BRPopLPushAsync(string source, string destination, int timeout) => MultiAsync(ListCommands.BRPopLPush(source, destination, timeout));

        public string LIndex(string key, long index) => Multi(ListCommands.LIndex(key, index));

        public Task<string> LIndexAsync(string key, long index) => MultiAsync(ListCommands.LIndex(key, index));

        public long LInsert(string key, RedisInsert insertType, string pivot, object value) => Multi(ListCommands.LInsert(key, insertType, pivot, value));

        public Task<long> LInsertAsync(string key, RedisInsert insertType, string pivot, object value) => MultiAsync(ListCommands.LInsert(key, insertType, pivot, value));

        public long LLen(string key) => Multi(ListCommands.LLen(key));

        public Task<long> LLenAsync(string key) => MultiAsync(ListCommands.LLen(key));

        public string LPop(string key) => Multi(ListCommands.LPop(key));

        public Task<string> LPopAsync(string key) => MultiAsync(ListCommands.LPop(key));

        public long LPush(string key, params object[] values) => Multi(ListCommands.LPush(key, values));

        public Task<long> LPushAsync(string key, params object[] values) => MultiAsync(ListCommands.LPush(key, values));

        public long LPushX(string key, object value) => Multi(ListCommands.LPushX(key, value));

        public Task<long> LPushXAsync(string key, object value) => MultiAsync(ListCommands.LPushX(key, value));

        public string[] LRange(string key, long start, long stop) => Multi(ListCommands.LRange(key, start, stop));

        public Task<string[]> LRangeAsync(string key, long start, long stop) => MultiAsync(ListCommands.LRange(key, start, stop));

        public long LRem(string key, long count, object value) => Multi(ListCommands.LRem(key, count, value));

        public Task<long> LRemAsync(string key, long count, object value) => MultiAsync(ListCommands.LRem(key, count, value));

        public string LSet(string key, long index, object value) => Multi(ListCommands.LSet(key, index, value));

        public Task<string> LSetAsync(string key, long index, object value) => MultiAsync(ListCommands.LSet(key, index, value));

        public string LTrim(string key, long start, long stop) => Multi(ListCommands.LTrim(key, start, stop));

        public Task<string> LTrimAsync(string key, long start, long stop) => MultiAsync(ListCommands.LTrim(key, start, stop));

        public string RPop(string key) => Multi(ListCommands.RPop(key));

        public Task<string> RPopAsync(string key) => MultiAsync(ListCommands.RPop(key));

        public string RPopLPush(string source, string destination) => Multi(ListCommands.RPopLPush(source, destination));

        public Task<string> RPopLPushAsync(string source, string destination) => MultiAsync(ListCommands.RPopLPush(source, destination));

        public long RPush(string key, params object[] values) => Multi(ListCommands.RPush(key, values));

        public Task<long> RPushAsync(string key, params object[] values) => MultiAsync(ListCommands.RPush(key, values));

        public long RPushX(string key, params object[] values) => Multi(ListCommands.RPushX(key, values));

        public Task<long> RPushXAsync(string key, params object[] values) => MultiAsync(ListCommands.RPushX(key, values));

        #endregion

        #region Sets

        public long SAdd(string key, params object[] members) => Multi(SetCommands.SAdd(key, members));

        public Task<long> SAddAsync(string key, params object[] members) => MultiAsync(SetCommands.SAdd(key, members));

        public long SCard(string key) => Multi(SetCommands.SCard(key));

        public Task<long> SCardAsync(string key) => MultiAsync(SetCommands.SCard(key));

        public string[] SDiff(params string[] keys) => Multi(SetCommands.SDiff(keys));

        public Task<string[]> SDiffAsync(params string[] keys) => MultiAsync(SetCommands.SDiff(keys));

        public long SDiffStore(string destination, params string[] keys) => Multi(SetCommands.SDiffStore(destination, keys));

        public Task<long> SDiffStoreAsync(string destination, params string[] keys) => MultiAsync(SetCommands.SDiffStore(destination, keys));

        public string[] SInter(params string[] keys) => Multi(SetCommands.SInter(keys));

        public Task<string[]> SInterAsync(params string[] keys) => MultiAsync(SetCommands.SInter(keys));

        public long SInterStore(string destination, params string[] keys) => Multi(SetCommands.SInterStore(destination, keys));

        public Task<long> SInterStoreAsync(string destination, params string[] keys) => MultiAsync(SetCommands.SInterStore(destination, keys));

        public bool SIsMember(string key, object member) => Multi(SetCommands.SIsMember(key, member));

        public Task<bool> SIsMemberAsync(string key, object member) => MultiAsync(SetCommands.SIsMember(key, member));

        public string[] SMembers(string key) => Multi(SetCommands.SMembers(key));

        public Task<string[]> SMembersAsync(string key) => MultiAsync(SetCommands.SMembers(key));

        public bool SMove(string source, string destination, object member) => Multi(SetCommands.SMove(source, destination, member));

        public Task<bool> SMoveAsync(string source, string destination, object member) => MultiAsync(SetCommands.SMove(source, destination, member));

        public string SPop(string key) => Multi(SetCommands.SPop(key));

        public Task<string> SPopAsync(string key) => MultiAsync(SetCommands.SPop(key));

        public string SRandMember(string key) => Multi(SetCommands.SRandMember(key));

        public Task<string> SRandMemberAsync(string key) => MultiAsync(SetCommands.SRandMember(key));

        public string[] SRandMember(string key, long count) => Multi(SetCommands.SRandMember(key, count));

        public Task<string[]> SRandMemberAsync(string key, long count) => MultiAsync(SetCommands.SRandMember(key, count));

        public long SRem(string key, params object[] members) => Multi(SetCommands.SRem(key, members));

        public Task<long> SRemAsync(string key, params object[] members) => MultiAsync(SetCommands.SRem(key, members));

        public string[] SUnion(params string[] keys) => Multi(SetCommands.SUnion(keys));

        public Task<string[]> SUnionAsync(params string[] keys) => MultiAsync(SetCommands.SUnion(keys));

        public long SUnionStore(string destination, params string[] keys) => Multi(SetCommands.SUnionStore(destination, keys));

        public Task<long> SUnionStoreAsync(string destination, params string[] keys) => MultiAsync(SetCommands.SUnionStore(destination, keys));

        #endregion

        #region Strings

        public long Append(string key, string value) => Multi(StringCommands.Append(key, value));

        public Task<long> AppendAsync(string key, string value) => MultiAsync(StringCommands.Append(key, value));

        public long BitCount(string key, long? start = null, long? end = null) => Multi(StringCommands.BitCount(key, start, end));

        public Task<long> BitCountAsync(string key, long? start = null, long? end = null) => MultiAsync(StringCommands.BitCount(key, start, end));

        public long BitOp(RedisBitOp operation, string destKey, params string[] keys) => Multi(StringCommands.BitOp(operation, destKey, keys));

        public Task<long> BitOpAsync(RedisBitOp operation, string destKey, params string[] keys) => MultiAsync(StringCommands.BitOp(operation, destKey, keys));

        public long Decr(string key) => Multi(StringCommands.Decr(key));

        public Task<long> DecrAsync(string key) => MultiAsync(StringCommands.Decr(key));

        public long DecrBy(string key, long decrement) => Multi(StringCommands.DecrBy(key, decrement));

        public Task<long> DecrByAsync(string key, long decrement) => MultiAsync(StringCommands.DecrBy(key, decrement));

        public string Get(string key) => Multi(StringCommands.Get(key));

        public Task<string> GetAsync(string key) => MultiAsync(StringCommands.Get(key));

        public bool GetBit(string key, uint offset) => Multi(StringCommands.GetBit(key, offset));

        public Task<bool> GetBitAsync(string key, uint offset) => MultiAsync(StringCommands.GetBit(key, offset));

        public string GetRange(string key, long start, long end) => Multi(StringCommands.GetRange(key, start, end));

        public Task<string> GetRangeAsync(string key, long start, long end) => MultiAsync(StringCommands.GetRange(key, start, end));

        public string GetSet(string key, object value) => Multi(StringCommands.GetSet(key, value));

        public Task<string> GetSetAsync(string key, object value) => MultiAsync(StringCommands.GetSet(key, value));

        public long Incr(string key) => Multi(StringCommands.Incr(key));

        public Task<long> IncrAsync(string key) => MultiAsync(StringCommands.Incr(key));

        public long IncrBy(string key, long increment) => Multi(StringCommands.IncrBy(key, increment));

        public Task<long> IncrByAsync(string key, long increment) => MultiAsync(StringCommands.IncrBy(key, increment));

        public double? IncrByFloat(string key, double increment) => Multi(StringCommands.IncrByFloat(key, increment));

        public Task<double?> IncrByFloatAsync(string key, double increment) => MultiAsync(StringCommands.IncrByFloat(key, increment));

        public string[] MGet(params string[] keys) => Multi(StringCommands.MGet(keys));

        public Task<string[]> MGetAsync(params string[] keys) => MultiAsync(StringCommands.MGet(keys));

        public string MSet(params string[] keyValues) => Multi(StringCommands.MSet(keyValues));

        public Task<string> MSetAsync(params string[] keyValues) => MultiAsync(StringCommands.MSet(keyValues));

        public bool MSetNx(params string[] keyValues) => Multi(StringCommands.MSetNx(keyValues));

        public Task<bool> MSetNxAsync(params string[] keyValues) => MultiAsync(StringCommands.MSetNx(keyValues));

        public string PSetEx(string key, long milliseconds, object value) => Multi(StringCommands.PSetEx(key, milliseconds, value));

        public Task<string> PSetExAsync(string key, long milliseconds, object value) => MultiAsync(StringCommands.PSetEx(key, milliseconds, value));

        public string Set(string key, object value) => Multi(StringCommands.Set(key, value));

        public Task<string> SetAsync(string key, object value) => MultiAsync(StringCommands.Set(key, value));

        public string Set(string key, object value, TimeSpan expiration, RedisExistence? condition = null) => Multi(StringCommands.Set(key, value, (int)expiration.TotalSeconds, null, condition));

        public Task<string> SetAsync(string key, object value, TimeSpan expiration, RedisExistence? condition = null) => MultiAsync(StringCommands.Set(key, value, (int)expiration.TotalSeconds, null, condition));

        public string Set(string key, object value, int? expirationSeconds = null, RedisExistence? condition = null) => Multi(StringCommands.Set(key, value, expirationSeconds, null, condition));

        public Task<string> SetAsync(string key, object value, int? expirationSeconds = null, RedisExistence? condition = null) => MultiAsync(StringCommands.Set(key, value, expirationSeconds, null, condition));

        public string Set(string key, object value, long? expirationMilliseconds = null, RedisExistence? condition = null) => Multi(StringCommands.Set(key, value, null, expirationMilliseconds, condition));

        public Task<string> SetAsync(string key, object value, long? expirationMilliseconds = null, RedisExistence? condition = null) => MultiAsync(StringCommands.Set(key, value, null, expirationMilliseconds, condition));

        public bool SetBit(string key, uint offset, bool value) => Multi(StringCommands.SetBit(key, offset, value));

        public Task<bool> SetBitAsync(string key, uint offset, bool value) => MultiAsync(StringCommands.SetBit(key, offset, value));

        public string SetEx(string key, long seconds, object value) => Multi(StringCommands.SetEx(key, seconds, value));

        public Task<string> SetExAsync(string key, long seconds, object value) => MultiAsync(StringCommands.SetEx(key, seconds, value));

        public bool SetNx(string key, object value) => Multi(StringCommands.SetNx(key, value));

        public Task<bool> SetNxAsync(string key, object value) => MultiAsync(StringCommands.SetNx(key, value));

        public long SetRange(string key, uint offset, object value) => Multi(StringCommands.SetRange(key, offset, value));

        public Task<long> SetRangeAsync(string key, uint offset, object value) => MultiAsync(StringCommands.SetRange(key, offset, value));

        public long StrLen(string key) => Multi(StringCommands.StrLen(key));

        public Task<long> StrLenAsync(string key) => MultiAsync(StringCommands.StrLen(key));

        #endregion

        public void Dispose()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                RedisClient client;
                if (_pool.TryDequeue(out client))
                    client.Dispose();
            }
        }
    }
}
