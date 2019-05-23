using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Sino.CacheStore.Events;
using Sino.CacheStore.Handler;
using Sino.CacheStore.Internal;
using Sino.Serializer.Abstractions;

namespace Sino.CacheStore
{
    public class CacheStoreClient : ICacheStore
    {
        private CommandFactory _cmdFactory;
        private IStoreHandler _handler;
        private IConvertProvider _convertProvider;

        public event EventHandler<QueryEventArgs> OnQuery;
        public event EventHandler<ChangeEventArgs> OnChange;
        public event EventHandler<RemoveEventArgs> OnRemove;

        public CacheStoreClient(CommandFactory cmdFactory, IStoreHandler handler, IConvertProvider convertProvider)
        {
            _cmdFactory = cmdFactory;
            _handler = handler;
            _convertProvider = convertProvider;
        }

        private void QueryEvent(string key, OperatorType opType, string command)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(command))
                throw new ArgumentNullException(nameof(command));

            var query = new QueryEventArgs(key, opType, command);
            OnQuery?.Invoke(this, query);
        }

        private void ChangeEvent(string key, OperatorType opType, string command)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(command))
                throw new ArgumentNullException(nameof(command));

            var change = new ChangeEventArgs(key, opType, command);
            OnChange?.Invoke(this, change);
        }

        private void RemoveEvent(string key, OperatorType opType, string command)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(command))
                throw new ArgumentNullException(nameof(command));

            var remove = new RemoveEventArgs(key, opType, command);
            OnRemove?.Invoke(this, remove);
        }

        public async Task Init()
        {
            await _handler.Init();
        }

        public long BitCount(string key, long? start = null, long? end = null)
        {
            return BitCountAsync(key, start, end).Result;
        }

        public async Task<long> BitCountAsync(string key, long? start = null, long? end = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (start != null && end != null && start > end)
                throw new IndexOutOfRangeException(nameof(end));

            QueryEvent(key, OperatorType.BitAndNumber, nameof(BitCount));
            var cmd = _cmdFactory.CreateBitCountCommand(key, start, end);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public long Decr(string key)
        {
            return DecrAsync(key).Result;
        }

        public async Task<long> DecrAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            ChangeEvent(key, OperatorType.BitAndNumber, nameof(DecrAsync));
            var cmd = _cmdFactory.CreateDecrCommand(key);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public long DecrBy(string key, long decrement)
        {
            return DecrByAsync(key, decrement).Result;
        }

        public async Task<long> DecrByAsync(string key, long decrement)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            ChangeEvent(key, OperatorType.BitAndNumber, nameof(DecrByAsync));
            var cmd = _cmdFactory.CreateDecrByCommand(key, decrement);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public bool Exists(string key)
        {
            return ExistsAsync(key).Result;
        }

        public async Task<bool> ExistsAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            QueryEvent(key, OperatorType.Normal, nameof(ExistsAsync));
            var cmd = _cmdFactory.CreateExistsCommand(key);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public bool Expire(string key, long value, bool isSeconds = true)
        {
            return ExpireAsync(key, value, isSeconds).Result;
        }

        public async Task<bool> ExpireAsync(string key, long value, bool isSeconds = true)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            ChangeEvent(key, OperatorType.Normal, nameof(ExpireAsync));
            CacheStoreCommand<bool> cmd = null;
            if (isSeconds)
            {
                cmd = _cmdFactory.CreateExpireCommand(key, (int)value);
            }
            else
            {
                cmd = _cmdFactory.CreatePExpireCommand(key, value);
            }
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            QueryEvent(key, OperatorType.Normal, nameof(GetAsync));
            var cmd = _cmdFactory.CreateGetCommand(key);
            var result = await _handler.ProcessAsync(cmd);

            var obj = _convertProvider.DeserializeByte<T>(result.Result);

            return obj;
        }

        public bool GetBit(string key, uint offset)
        {
            return GetBitAsync(key, offset).Result;
        }

        public async Task<bool> GetBitAsync(string key, uint offset)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            QueryEvent(key, OperatorType.BitAndNumber, nameof(GetBitAsync));
            var cmd = _cmdFactory.CreateGetBitCommand(key, offset);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public long HDel(string key, params string[] fields)
        {
            return HDelAsync(key, fields).Result;
        }

        public async Task<long> HDelAsync(string key, params string[] fields)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));
            if (fields.Length <= 0)
                throw new ArgumentNullException(nameof(fields));

            RemoveEvent(key, OperatorType.Hash, nameof(HDelAsync));
            var cmd = _cmdFactory.CreateHDelCommand(key, fields);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public bool HExists(string key, string field)
        {
            return HExistsAsync(key, field).Result;
        }

        public async Task<bool> HExistsAsync(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            QueryEvent(key, OperatorType.Hash, nameof(HExistsAsync));
            var cmd = _cmdFactory.CreateHExistsCommand(key, field);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public T HGet<T>(string key, string field)
        {
            return HGetAsync<T>(key, field).Result;
        }

        public async Task<T> HGetAsync<T>(string key, string field)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            QueryEvent(key, OperatorType.Hash, nameof(HGetAsync));
            var cmd = _cmdFactory.CreateHGetCommand(key, field);
            var result = await _handler.ProcessAsync(cmd);

            var obj = _convertProvider.DeserializeByte<T>(result.Result);

            return obj;
        }

        public long HLen(string key)
        {
            return HLenAsync(key).Result;
        }

        public async Task<long> HLenAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            QueryEvent(key, OperatorType.Hash, nameof(HLenAsync));
            var cmd = _cmdFactory.CreateHLenCommand(key);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public bool HSet<T>(string key, string field, T value)
        {
            return HSetAsync(key, field, value).Result;
        }

        public async Task<bool> HSetAsync<T>(string key, string field, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            ChangeEvent(key, OperatorType.Hash, nameof(HSetAsync));
            var bytes = _convertProvider.SerializeByte(value);
            var cmd = _cmdFactory.CreateHSetCommand(key, field, bytes);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public bool HSetWithNoExisted<T>(string key, string field, T value)
        {
            return HSetWithNoExistedAsync(key, field, value).Result;
        }

        public async Task<bool> HSetWithNoExistedAsync<T>(string key, string field, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (string.IsNullOrEmpty(field))
                throw new ArgumentNullException(nameof(field));

            ChangeEvent(key, OperatorType.Hash, nameof(HSetWithNoExistedAsync));
            var bytes = _convertProvider.SerializeByte(value);
            var cmd = _cmdFactory.CreateHSetWithNoExistCommand(key, field, bytes);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public long Incr(string key)
        {
            return IncrAsync(key).Result;
        }

        public async Task<long> IncrAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            ChangeEvent(key, OperatorType.BitAndNumber, nameof(IncrAsync));
            var cmd = _cmdFactory.CreateIncrCommand(key);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public long IncrBy(string key, long increment)
        {
            return IncrByAsync(key, increment).Result;
        }

        public async Task<long> IncrByAsync(string key, long increment)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            ChangeEvent(key, OperatorType.BitAndNumber, nameof(IncrByAsync));
            var cmd = _cmdFactory.CreateIncrByCommand(key, increment);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public T LIndex<T>(string key, long index)
        {
            return LIndexAsync<T>(key, index).Result;
        }

        public async Task<T> LIndexAsync<T>(string key, long index)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            QueryEvent(key, OperatorType.List, nameof(LIndexAsync));
            var cmd = _cmdFactory.CreateLIndexCommand(key, index);
            var result = await _handler.ProcessAsync(cmd);
            var obj = _convertProvider.DeserializeByte<T>(result.Result);

            return obj;
        }

        public long LLen(string key)
        {
            return LLenAsync(key).Result;
        }

        public async Task<long> LLenAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            QueryEvent(key, OperatorType.List, nameof(LLenAsync));
            var cmd = _cmdFactory.CreateLLenCommand(key);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public T LPop<T>(string key)
        {
            return LPopAsync<T>(key).Result;
        }

        public async Task<T> LPopAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            QueryEvent(key, OperatorType.List, nameof(LPopAsync));
            var cmd = _cmdFactory.CreateLPopCommand(key);
            var result = await _handler.ProcessAsync(cmd);
            var obj = _convertProvider.DeserializeByte<T>(result.Result);

            return obj;
        }

        public long LPush<T>(string key, params T[] values)
        {
            return LPushAsync<T>(key, values.ToArray()).Result;
        }

        public async Task<long> LPushAsync<T>(string key, params T[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null || values?.Length <= 0)
                throw new ArgumentNullException(nameof(values));

            ChangeEvent(key, OperatorType.List, nameof(LPushAsync));
            var bytes = values.Select(x => _convertProvider.SerializeByte(x));
            var cmd = _cmdFactory.CreateLPushCommand(key, values.ToArray());
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public long Remove(string key)
        {
            return RemoveAsync(key).Result;
        }

        public async Task<long> RemoveAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            RemoveEvent(key, OperatorType.Normal, nameof(RemoveAsync));
            var cmd = _cmdFactory.CreateRemoveCommand(key);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public T RPop<T>(string key)
        {
            return RPopAsync<T>(key).Result;
        }

        public async Task<T> RPopAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            ChangeEvent(key, OperatorType.List, nameof(RPopAsync));
            var cmd = _cmdFactory.CreateRPopCommand(key);
            var result = await _handler.ProcessAsync(cmd);

            var obj = _convertProvider.DeserializeByte<T>(result.Result);

            return obj;
        }

        public long RPush<T>(string key, params T[] values)
        {
            return RPushAsync<T>(key, values.ToArray()).Result;
        }

        public async Task<long> RPushAsync<T>(string key, params T[] values)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (values == null || values?.Length <= 0)
                throw new ArgumentNullException(nameof(values));

            ChangeEvent(key, OperatorType.List, nameof(RPushAsync));
            var bytes = values.Select(x => _convertProvider.SerializeByte(x));
            var cmd = _cmdFactory.CreateRPushCommand(key, bytes.ToArray());
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public string Set<T>(string key, T value, int? timeout = null)
        {
            return SetAsync(key, value, timeout).Result;
        }

        public async Task<string> SetAsync<T>(string key, T value, int? timeout = null)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            ChangeEvent(key, OperatorType.Normal, nameof(SetAsync));
            var bytes = _convertProvider.SerializeByte(value);
            var cmd = _cmdFactory.CreateSetCommand(key, bytes, timeout);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public bool SetBit(string key, uint offset, bool value)
        {
            return SetBitAsync(key, offset, value).Result;
        }

        public async Task<bool> SetBitAsync(string key, uint offset, bool value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            ChangeEvent(key, OperatorType.BitAndNumber, nameof(SetBitAsync));
            var cmd = _cmdFactory.CreateSetBitCommand(key, offset, value);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }

        public string SetWithNoExisted<T>(string key, T value)
        {
            return SetWithNoExistedAsync(key, value).Result;
        }

        public async Task<string> SetWithNoExistedAsync<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            ChangeEvent(key, OperatorType.Normal, nameof(SetWithNoExistedAsync));
            var bytes = _convertProvider.SerializeByte(value);
            var cmd = _cmdFactory.CreateSetCommand(key, bytes, null, null, CacheStoreExistence.Nx);
            var result = await _handler.ProcessAsync(cmd);

            return result.Result;
        }
    }
}
