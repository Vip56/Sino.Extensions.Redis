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

        public string Echo(string message)
        {
            return Multi(ConnectionCommands.Echo(message));
        }

        public Task<string> EchoAsync(string message)
        {
            return MultiAsync(ConnectionCommands.Echo(message));
        }

        public string Ping()
        {
            return Multi(ConnectionCommands.Ping());
        }

        public Task<string> PingAsync()
        {
            return MultiAsync(ConnectionCommands.Ping());
        }

        public string Quit()
        {
            return Multi(ConnectionCommands.Quit());
        }

        public Task<string> QuitAsync()
        {
            return MultiAsync(ConnectionCommands.Quit());
        }

        public string Select(int index)
        {
            return Multi(ConnectionCommands.Select(index));
        }

        public Task<string> SelectAsync(int index)
        {
            return MultiAsync(ConnectionCommands.Select(index));
        }

        #endregion
        
        #region Keys

        public long Del(params string[] keys)
        {
            return Multi(KeyCommands.Del(keys));
        }

        public Task<long> DelAsync(params string[] keys)
        {
            return MultiAsync(KeyCommands.Del(keys));
        }

        public byte[] Dump(string key)
        {
            return Multi(KeyCommands.Dump(key));
        }

        public Task<byte[]> DumpAsync(string key)
        {
            return MultiAsync(KeyCommands.Dump(key));
        }

        public bool Exists(string key)
        {
            return Multi(KeyCommands.Exists(key));
        }

        public Task<bool> ExistsAsync(string key)
        {
            return MultiAsync(KeyCommands.Exists(key));
        }

        public bool Expire(string key, int seconds)
        {
            return Multi(KeyCommands.Expire(key, seconds));
        }

        public Task<bool> ExpireAsync(string key, int seconds)
        {
            return MultiAsync(KeyCommands.Expire(key, seconds));
        }

        public bool ExpireAt(string key, DateTime expirationDate)
        {
            return Multi(KeyCommands.ExpireAt(key, expirationDate.GetUnixTime() / 1000));
        }

        public Task<bool> ExpireAtAsync(string key, DateTime expirationDate)
        {
            return MultiAsync(KeyCommands.ExpireAt(key, expirationDate.GetUnixTime() / 1000));
        }

        public bool ExpireAt(string key, long timestamp)
        {
            return Multi(KeyCommands.ExpireAt(key, timestamp));
        }

        public Task<bool> ExpireAtAsync(string key, int timestamp)
        {
            return MultiAsync(KeyCommands.ExpireAt(key, timestamp));
        }

        public string[] Keys(string pattern)
        {
            return Multi(KeyCommands.Keys(pattern));
        }

        public Task<string[]> KeysAsync(string pattern)
        {
            return MultiAsync(KeyCommands.Keys(pattern));
        }

        public string Migrate(string host, int port, string key, int destinationDb, int timeoutMilliseconds)
        {
            return Multi(KeyCommands.Migrate(host, port, key, destinationDb, timeoutMilliseconds));
        }

        public Task<string> MigrateAsync(string host, int port, string key, int destinationDb, int timeoutMilliseconds)
        {
            return MultiAsync(KeyCommands.Migrate(host, port, key, destinationDb, timeoutMilliseconds));
        }

        public string Migrate(string host, int port, string key, int destinationDb, TimeSpan timeout)
        {
            return Multi(KeyCommands.Migrate(host, port, key, destinationDb, (int)timeout.TotalMilliseconds));
        }

        public Task<string> MigrateAsync(string host, int port, string key, int destinationDb, TimeSpan timeout)
        {
            return MultiAsync(KeyCommands.Migrate(host, port, key, destinationDb, (int)timeout.TotalMilliseconds));
        }

        public bool Move(string key, int database)
        {
            return Multi(KeyCommands.Move(key, database));
        }

        public Task<bool> MoveAsync(string key, int database)
        {
            return MultiAsync(KeyCommands.Move(key, database));
        }

        public long ObjectIdleTime(string key)
        {
            return Multi(KeyCommands.ObjectIdleTime(key));
        }

        public Task<long> ObjectIdleTimeAsync(string key)
        {
            return MultiAsync(KeyCommands.ObjectIdleTime(key));
        }

        public bool Persist(string key)
        {
            return Multi(KeyCommands.Persist(key));
        }

        public Task<bool> PersistAsync(string key)
        {
            return MultiAsync(KeyCommands.Persist(key));
        }

        public bool PExpire(string key, TimeSpan expiration)
        {
            return Multi(KeyCommands.PExpire(key, (long)expiration.TotalMilliseconds));
        }

        public Task<bool> PExpireAsync(string key, TimeSpan expiration)
        {
            return MultiAsync(KeyCommands.PExpire(key, (long)expiration.TotalMilliseconds));
        }

        public bool PExpire(string key, long milliseconds)
        {
            return Multi(KeyCommands.PExpire(key, milliseconds));
        }

        public Task<bool> PExpireAsync(string key, long milliseconds)
        {
            return MultiAsync(KeyCommands.PExpire(key, milliseconds));
        }

        public bool PExpireAt(string key, DateTime date)
        {
            return Multi(KeyCommands.PExpireAt(key, date.GetUnixTime()));
        }

        public Task<bool> PExpireAtAsync(string key, DateTime date)
        {
            return MultiAsync(KeyCommands.PExpireAt(key, date.GetUnixTime()));
        }

        public bool PExpireAt(string key, long timestamp)
        {
            return Multi(KeyCommands.PExpireAt(key, timestamp));
        }

        public Task<bool> PExpireAtAsync(string key, long timestamp)
        {
            return MultiAsync(KeyCommands.PExpireAt(key, timestamp));
        }

        public long PTtl(string key)
        {
            return Multi(KeyCommands.PTtl(key));
        }

        public Task<long> PTtlAsync(string key)
        {
            return MultiAsync(KeyCommands.PTtl(key));
        }

        public string RandomKey()
        {
            return Multi(KeyCommands.RandomKey());
        }

        public Task<string> RandomKeyAsync()
        {
            return MultiAsync(KeyCommands.RandomKey());
        }

        public string Rename(string key, string newKey)
        {
            return Multi(KeyCommands.Rename(key, newKey));
        }

        public Task<string> RenameAsync(string key, string newKey)
        {
            return MultiAsync(KeyCommands.Rename(key, newKey));
        }

        public bool RenameNx(string key, string newKey)
        {
            return Multi(KeyCommands.RenameNx(key, newKey));
        }

        public Task<bool> RenameNxAsync(string key, string newKey)
        {
            return MultiAsync(KeyCommands.RenameNx(key, newKey));
        }

        public string Restore(string key, long ttl, string serializedValue)
        {
            return Multi(KeyCommands.Restore(key, ttl, serializedValue));
        }

        public Task<string> RestoreAsync(string key, long ttl, string serializedValue)
        {
            return MultiAsync(KeyCommands.Restore(key, ttl, serializedValue));
        }

        public long Ttl(string key)
        {
            return Multi(KeyCommands.Ttl(key));
        }

        public Task<long> TtlAsync(string key)
        {
            return MultiAsync(KeyCommands.Ttl(key));
        }

        #endregion

        #region Hashes
        
        public long HDel(string key, params string[] fields)
        {
            return Multi(HashCommands.HDel(key, fields));
        }

        public Task<long> HDelAsync(string key, params string[] fields)
        {
            return MultiAsync(HashCommands.HDel(key, fields));
        }

        public bool HExists(string key, string field)
        {
            return Multi(HashCommands.HExists(key, field));
        }

        public Task<bool> HExistsAsync(string key, string field)
        {
            return MultiAsync(HashCommands.HExists(key, field));
        }

        public string HGet(string key, string field)
        {
            return Multi(HashCommands.HGet(key, field));
        }

        public Task<string> HGetAsync(string key, string field)
        {
            return MultiAsync(HashCommands.HGet(key, field));
        }

        public Dictionary<string, string> HGetAll(string key)
        {
            return Multi(HashCommands.HGetAll(key));
        }

        public Task<Dictionary<string, string>> HGetAllAsync(string key)
        {
            return MultiAsync(HashCommands.HGetAll(key));
        }

        public long HIncrBy(string key, string field, long increment)
        {
            return Multi(HashCommands.HIncrBy(key, field, increment));
        }

        public Task<long> HIncrByAsync(string key, string field, long increment)
        {
            return MultiAsync(HashCommands.HIncrBy(key, field, increment));
        }

        public double? HIncrByFloat(string key, string field, double increment)
        {
            return Multi(HashCommands.HIncrByFloat(key, field, increment));
        }

        public Task<double?> HIncrByFloatAsync(string key, string field, double increment)
        {
            return MultiAsync(HashCommands.HIncrByFloat(key, field, increment));
        }

        public string[] HKeys(string key)
        {
            return Multi(HashCommands.HKeys(key));
        }

        public Task<string[]> HKeysAsync(string key)
        {
            return MultiAsync(HashCommands.HKeys(key));
        }

        public long HLen(string key)
        {
            return Multi(HashCommands.HLen(key));
        }

        public Task<long> HLenAsync(string key)
        {
            return MultiAsync(HashCommands.HLen(key));
        }

        public string[] HMGet(string key, params string[] fields)
        {
            return Multi(HashCommands.HMGet(key, fields));
        }

        public string HMSet(string key, Dictionary<string, string> dict)
        {
            return Multi(HashCommands.HMSet(key, dict));
        }

        public Task<string> HMSetAsync(string key, Dictionary<string, string> dict)
        {
            return MultiAsync(HashCommands.HMSet(key, dict));
        }

        public bool HSet(string key, string field, object value)
        {
            return Multi(HashCommands.HSet(key, field, value));
        }

        public Task<bool> HSetAsync(string key, string field, object value)
        {
            return MultiAsync(HashCommands.HSet(key, field, value));
        }

        public bool HSetNx(string key, string field, object value)
        {
            return Multi(HashCommands.HSetNx(key, field, value));
        }

        public Task<bool> HSetNxAsync(string key, string field, object value)
        {
            return MultiAsync(HashCommands.HSetNx(key, field, value));
        }

        #endregion

        #region Lists
        
        public Tuple<string, string> BLPop(int timeout, params string[] keys)
        {
            return Multi(ListCommands.BLPop(timeout, keys));
        }

        public Task<Tuple<string, string>> BLPopAsync(int timeout, params string[] keys)
        {
            return MultiAsync(ListCommands.BLPop(timeout, keys));
        }

        public Tuple<string, string> BLPop(TimeSpan timeout, params string[] keys)
        {
            return Multi(ListCommands.BLPop((int)timeout.TotalSeconds , keys));
        }

        public Task<Tuple<string, string>> BLPopAsync(TimeSpan timeout, params string[] keys)
        {
            return MultiAsync(ListCommands.BLPop((int)timeout.TotalSeconds, keys));
        }

        public Tuple<string, string> BRPop(int timeout, params string[] keys)
        {
            return Multi(ListCommands.BRPop(timeout, keys));
        }

        public Task<Tuple<string, string>> BRPopAsync(int timeout, params string[] keys)
        {
            return MultiAsync(ListCommands.BRPop(timeout, keys));
        }

        public Tuple<string, string> BRPop(TimeSpan timeout, params string[] keys)
        {
            return Multi(ListCommands.BRPop((int)timeout.TotalSeconds, keys));
        }

        public Task<Tuple<string, string>> BRPopAsync(TimeSpan timeout, params string[] keys)
        {
            return MultiAsync(ListCommands.BRPop((int)timeout.TotalSeconds, keys));
        }

        public string BRPopLPush(string source, string destination, int timeout)
        {
            return Multi(ListCommands.BRPopLPush(source, destination, timeout));
        }

        public Task<string> BRPopLPushAsync(string source, string destination, int timeout)
        {
            return MultiAsync(ListCommands.BRPopLPush(source, destination, timeout));
        }

        public string LIndex(string key, long index)
        {
            return Multi(ListCommands.LIndex(key, index));
        }

        public Task<string> LIndexAsync(string key, long index)
        {
            return MultiAsync(ListCommands.LIndex(key, index));
        }

        public long LInsert(string key, RedisInsert insertType, string pivot, object value)
        {
            return Multi(ListCommands.LInsert(key, insertType, pivot, value));
        }

        public Task<long> LInsertAsync(string key, RedisInsert insertType, string pivot, object value)
        {
            return MultiAsync(ListCommands.LInsert(key, insertType, pivot, value));
        }

        public long LLen(string key)
        {
            return Multi(ListCommands.LLen(key));
        }

        public Task<long> LLenAsync(string key)
        {
            return MultiAsync(ListCommands.LLen(key));
        }

        public string LPop(string key)
        {
            return Multi(ListCommands.LPop(key));
        }

        public Task<string> LPopAsync(string key)
        {
            return MultiAsync(ListCommands.LPop(key));
        }

        public long LPush(string key, params object[] values)
        {
            return Multi(ListCommands.LPush(key, values));
        }

        public Task<long> LPushAsync(string key, params object[] values)
        {
            return MultiAsync(ListCommands.LPush(key, values));
        }

        public long LPushX(string key, object value)
        {
            return Multi(ListCommands.LPushX(key, value));
        }

        public Task<long> LPushXAsync(string key, object value)
        {
            return MultiAsync(ListCommands.LPushX(key, value));
        }

        public string[] LRange(string key, long start, long stop)
        {
            return Multi(ListCommands.LRange(key, start, stop));
        }

        public Task<string[]> LRangeAsync(string key, long start, long stop)
        {
            return MultiAsync(ListCommands.LRange(key, start, stop));
        }

        public long LRem(string key, long count, object value)
        {
            return Multi(ListCommands.LRem(key, count, value));
        }

        public Task<long> LRemAsync(string key, long count, object value)
        {
            return MultiAsync(ListCommands.LRem(key, count, value));
        }

        public string LSet(string key, long index, object value)
        {
            return Multi(ListCommands.LSet(key, index, value));
        }

        public Task<string> LSetAsync(string key, long index, object value)
        {
            return MultiAsync(ListCommands.LSet(key, index, value));
        }

        public string LTrim(string key, long start, long stop)
        {
            return Multi(ListCommands.LTrim(key, start, stop));
        }

        public Task<string> LTrimAsync(string key, long start, long stop)
        {
            return MultiAsync(ListCommands.LTrim(key, start, stop));
        }

        public string RPop(string key)
        {
            return Multi(ListCommands.RPop(key));
        }

        public Task<string> RPopAsync(string key)
        {
            return MultiAsync(ListCommands.RPop(key));
        }

        public string RPopLPush(string source, string destination)
        {
            return Multi(ListCommands.RPopLPush(source, destination));
        }

        public Task<string> RPopLPushAsync(string source, string destination)
        {
            return MultiAsync(ListCommands.RPopLPush(source, destination));
        }

        public long RPush(string key, params object[] values)
        {
            return Multi(ListCommands.RPush(key, values));
        }

        public Task<long> RPushAsync(string key, params object[] values)
        {
            return MultiAsync(ListCommands.RPush(key, values));
        }

        public long RPushX(string key, params object[] values)
        {
            return Multi(ListCommands.RPushX(key, values));
        }

        public Task<long> RPushXAsync(string key, params object[] values)
        {
            return MultiAsync(ListCommands.RPushX(key, values));
        }

        #endregion

        #region Sets

        public long SAdd(string key, params object[] members)
        {
            return Multi(SetCommands.SAdd(key, members));
        }

        public Task<long> SAddAsync(string key, params object[] members)
        {
            return MultiAsync(SetCommands.SAdd(key, members));
        }

        public long SCard(string key)
        {
            return Multi(SetCommands.SCard(key));
        }

        public Task<long> SCardAsync(string key)
        {
            return MultiAsync(SetCommands.SCard(key));
        }

        public string[] SDiff(params string[] keys)
        {
            return Multi(SetCommands.SDiff(keys));
        }

        public Task<string[]> SDiffAsync(params string[] keys)
        {
            return MultiAsync(SetCommands.SDiff(keys));
        }

        public long SDiffStore(string destination, params string[] keys)
        {
            return Multi(SetCommands.SDiffStore(destination, keys));
        }

        public Task<long> SDiffStoreAsync(string destination, params string[] keys)
        {
            return MultiAsync(SetCommands.SDiffStore(destination, keys));
        }

        public string[] SInter(params string[] keys)
        {
            return Multi(SetCommands.SInter(keys));
        }

        public Task<string[]> SInterAsync(params string[] keys)
        {
            return MultiAsync(SetCommands.SInter(keys));
        }

        public long SInterStore(string destination, params string[] keys)
        {
            return Multi(SetCommands.SInterStore(destination, keys));
        }

        public Task<long> SInterStoreAsync(string destination, params string[] keys)
        {
            return MultiAsync(SetCommands.SInterStore(destination, keys));
        }

        public bool SIsMember(string key, object member)
        {
            return Multi(SetCommands.SIsMember(key, member));
        }

        public Task<bool> SIsMemberAsync(string key, object member)
        {
            return MultiAsync(SetCommands.SIsMember(key, member));
        }

        public string[] SMembers(string key)
        {
            return Multi(SetCommands.SMembers(key));
        }

        public Task<string[]> SMembersAsync(string key)
        {
            return MultiAsync(SetCommands.SMembers(key));
        }

        public bool SMove(string source, string destination, object member)
        {
            return Multi(SetCommands.SMove(source, destination, member));
        }

        public Task<bool> SMoveAsync(string source, string destination, object member)
        {
            return MultiAsync(SetCommands.SMove(source, destination, member));
        }

        public string SPop(string key)
        {
            return Multi(SetCommands.SPop(key));
        }

        public Task<string> SPopAsync(string key)
        {
            return MultiAsync(SetCommands.SPop(key));
        }

        public string SRandMember(string key)
        {
            return Multi(SetCommands.SRandMember(key));
        }

        public Task<string> SRandMemberAsync(string key)
        {
            return MultiAsync(SetCommands.SRandMember(key));
        }

        public string[] SRandMember(string key, long count)
        {
            return Multi(SetCommands.SRandMember(key, count));
        }

        public Task<string[]> SRandMemberAsync(string key, long count)
        {
            return MultiAsync(SetCommands.SRandMember(key, count));
        }

        public long SRem(string key, params object[] members)
        {
            return Multi(SetCommands.SRem(key, members));
        }

        public Task<long> SRemAsync(string key, params object[] members)
        {
            return MultiAsync(SetCommands.SRem(key, members));
        }

        public string[] SUnion(params string[] keys)
        {
            return Multi(SetCommands.SUnion(keys));
        }

        public Task<string[]> SUnionAsync(params string[] keys)
        {
            return MultiAsync(SetCommands.SUnion(keys));
        }

        public long SUnionStore(string destination, params string[] keys)
        {
            return Multi(SetCommands.SUnionStore(destination, keys));
        }

        public Task<long> SUnionStoreAsync(string destination, params string[] keys)
        {
            return MultiAsync(SetCommands.SUnionStore(destination, keys));
        }

        #endregion

        #region Strings

        public long Append(string key, string value)
        {
            return Multi(StringCommands.Append(key, value));
        }

        public Task<long> AppendAsync(string key, string value)
        {
            return MultiAsync(StringCommands.Append(key, value));
        }

        public long BitCount(string key, long? start = null, long? end = null)
        {
            return Multi(StringCommands.BitCount(key, start, end));
        }

        public Task<long> BitCountAsync(string key, long? start = null, long? end = null)
        {
            return MultiAsync(StringCommands.BitCount(key, start, end));
        }

        public long BitOp(RedisBitOp operation, string destKey, params string[] keys)
        {
            return Multi(StringCommands.BitOp(operation, destKey, keys));
        }

        public Task<long> BitOpAsync(RedisBitOp operation, string destKey, params string[] keys)
        {
            return MultiAsync(StringCommands.BitOp(operation, destKey, keys));
        }

        public long Decr(string key)
        {
            return Multi(StringCommands.Decr(key));
        }

        public Task<long> DecrAsync(string key)
        {
            return MultiAsync(StringCommands.Decr(key));
        }

        public long DecrBy(string key, long decrement)
        {
            return Multi(StringCommands.DecrBy(key, decrement));
        }

        public Task<long> DecrByAsync(string key, long decrement)
        {
            return MultiAsync(StringCommands.DecrBy(key, decrement));
        }

        public string Get(string key)
        {
            return Multi(StringCommands.Get(key));
        }

        public Task<string> GetAsync(string key)
        {
            return MultiAsync(StringCommands.Get(key));
        }

        public bool GetBit(string key, uint offset)
        {
            return Multi(StringCommands.GetBit(key, offset));
        }

        public Task<bool> GetBitAsync(string key, uint offset)
        {
            return MultiAsync(StringCommands.GetBit(key, offset));
        }

        public string GetRange(string key, long start, long end)
        {
            return Multi(StringCommands.GetRange(key, start, end));
        }

        public Task<string> GetRangeAsync(string key, long start, long end)
        {
            return MultiAsync(StringCommands.GetRange(key, start, end));
        }

        public string GetSet(string key, object value)
        {
            return Multi(StringCommands.GetSet(key, value));
        }

        public Task<string> GetSetAsync(string key, object value)
        {
            return MultiAsync(StringCommands.GetSet(key, value));
        }

        public long Incr(string key)
        {
            return Multi(StringCommands.Incr(key));
        }

        public Task<long> IncrAsync(string key)
        {
            return MultiAsync(StringCommands.Incr(key));
        }

        public long IncrBy(string key, long increment)
        {
            return Multi(StringCommands.IncrBy(key, increment));
        }

        public Task<long> IncrByAsync(string key, long increment)
        {
            return MultiAsync(StringCommands.IncrBy(key, increment));
        }

        public double? IncrByFloat(string key, double increment)
        {
            return Multi(StringCommands.IncrByFloat(key, increment));


        }

        public Task<double?> IncrByFloatAsync(string key, double increment)
        {
            return MultiAsync(StringCommands.IncrByFloat(key, increment));
        }

        public string[] MGet(params string[] keys)
        {
            return Multi(StringCommands.MGet(keys));
        }

        public Task<string[]> MGetAsync(params string[] keys)
        {
            return MultiAsync(StringCommands.MGet(keys));
        }

        public string MSet(params string[] keyValues)
        {
            return Multi(StringCommands.MSet(keyValues));
        }

        public Task<string> MSetAsync(params string[] keyValues)
        {
            return MultiAsync(StringCommands.MSet(keyValues));
        }

        public bool MSetNx(params string[] keyValues)
        {
            return Multi(StringCommands.MSetNx(keyValues));
        }

        public Task<bool> MSetNxAsync(params string[] keyValues)
        {
            return MultiAsync(StringCommands.MSetNx(keyValues));
        }

        public string PSetEx(string key, long milliseconds, object value)
        {
            return Multi(StringCommands.PSetEx(key, milliseconds, value));
        }

        public Task<string> PSetExAsync(string key, long milliseconds, object value)
        {
            return MultiAsync(StringCommands.PSetEx(key, milliseconds, value));
        }

        public string Set(string key, object value)
        {
            return Multi(StringCommands.Set(key, value));
        }

        public Task<string> SetAsync(string key, object value)
        {
            return MultiAsync(StringCommands.Set(key, value));
        }

        public string Set(string key, object value, TimeSpan expiration, RedisExistence? condition = null)
        {
            return Multi(StringCommands.Set(key, value, (int)expiration.TotalSeconds, null, condition));
        }

        public Task<string> SetAsync(string key, object value, TimeSpan expiration, RedisExistence? condition = null)
        {
            return MultiAsync(StringCommands.Set(key, value, (int)expiration.TotalSeconds, null, condition));
        }

        public string Set(string key, object value, int? expirationSeconds = null, RedisExistence? condition = null)
        {
            return Multi(StringCommands.Set(key, value, expirationSeconds, null, condition));
        }

        public Task<string> SetAsync(string key, object value, int? expirationSeconds = null, RedisExistence? condition = null)
        {
            return MultiAsync(StringCommands.Set(key, value, expirationSeconds, null, condition));
        }

        public string Set(string key, object value, long? expirationMilliseconds = null, RedisExistence? condition = null)
        {
            return Multi(StringCommands.Set(key, value, null, expirationMilliseconds, condition));
        }

        public Task<string> SetAsync(string key, object value, long? expirationMilliseconds = null, RedisExistence? condition = null)
        {
            return MultiAsync(StringCommands.Set(key, value, null, expirationMilliseconds, condition));
        }

        public bool SetBit(string key, uint offset, bool value)
        {
            return Multi(StringCommands.SetBit(key, offset, value));
        }

        public Task<bool> SetBitAsync(string key, uint offset, bool value)
        {
            return MultiAsync(StringCommands.SetBit(key, offset, value));
        }

        public string SetEx(string key, long seconds, object value)
        {
            return Multi(StringCommands.SetEx(key, seconds, value));
        }

        public Task<string> SetExAsync(string key, long seconds, object value)
        {
            return MultiAsync(StringCommands.SetEx(key, seconds, value));
        }

        public bool SetNx(string key, object value)
        {
            return Multi(StringCommands.SetNx(key, value));
        }

        public Task<bool> SetNxAsync(string key, object value)
        {
            return MultiAsync(StringCommands.SetNx(key, value));
        }

        public long SetRange(string key, uint offset, object value)
        {
            return Multi(StringCommands.SetRange(key, offset, value));
        }

        public Task<long> SetRangeAsync(string key, uint offset, object value)
        {
            return MultiAsync(StringCommands.SetRange(key, offset, value));
        }

        public long StrLen(string key)
        {
            return Multi(StringCommands.StrLen(key));
        }

        public Task<long> StrLenAsync(string key)
        {
            return MultiAsync(StringCommands.StrLen(key));
        }

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
