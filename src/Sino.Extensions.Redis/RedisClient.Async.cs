using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sino.Extensions.Redis
{
    public partial class RedisClient
    {
        #region Hashes

        /// <summary>
        /// Delete one or more hash fields
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="fields">Fields to delete</param>
        /// <returns>Number of fields removed from hash</returns>
        public Task<long> HDelAsync(string key, params string[] fields)
        {
            return WriteAsync(RedisCommands.HDel(key, fields));
        }

        /// <summary>
        /// Determine if a hash field exists
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="field">Field to check</param>
        /// <returns>True if hash field exists</returns>
        public Task<bool> HExistsAsync(string key, string field)
        {
            return WriteAsync(RedisCommands.HExists(key, field));
        }

        /// <summary>
        /// Get the value of a hash field
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="field">Field to get</param>
        /// <returns>Value of hash field</returns>
        public Task<string> HGetAsync(string key, string field)
        {
            return WriteAsync(RedisCommands.HGet(key, field));
        }

        /// <summary>
        /// Get all the fields and values in a hash
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <returns>Dictionary mapped from string</returns>
        public Task<Dictionary<string, string>> HGetAllAsync(string key)
        {
            return WriteAsync(RedisCommands.HGetAll(key));
        }

        /// <summary>
        /// Increment the integer value of a hash field by the given number
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="field">Field to increment</param>
        /// <param name="increment">Increment value</param>
        /// <returns>Value of field after increment</returns>
        public Task<long> HIncrByAsync(string key, string field, long increment)
        {
            return WriteAsync(RedisCommands.HIncrBy(key, field, increment));
        }

        /// <summary>
        /// Increment the float value of a hash field by the given number
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="field">Field to increment</param>
        /// <param name="increment">Increment value</param>
        /// <returns>Value of field after increment</returns>
        public Task<double> HIncrByFloatAsync(string key, string field, double increment)
        {
            return WriteAsync(RedisCommands.HIncrByFloat(key, field, increment));
        }

        /// <summary>
        /// Get all the fields in a hash
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <returns>All hash field names</returns>
        public Task<string[]> HKeysAsync(string key)
        {
            return WriteAsync(RedisCommands.HKeys(key));
        }

        /// <summary>
        /// Get the number of fields in a hash
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <returns>Number of fields in hash</returns>
        public Task<long> HLenAsync(string key)
        {
            return WriteAsync(RedisCommands.HLen(key));
        }

        /// <summary>
        /// Get the values of all the given hash fields
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="fields">Fields to return</param>
        /// <returns>Values of given fields</returns>
        public Task<string[]> HMGetAsync(string key, params string[] fields)
        {
            return WriteAsync(RedisCommands.HMGet(key, fields));
        }

        /// <summary>
        /// Set multiple hash fields to multiple values
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="dict">Dictionary mapping of hash</param>
        /// <returns>Status code</returns>
        public Task<string> HMSetAsync(string key, Dictionary<string, string> dict)
        {
            return WriteAsync(RedisCommands.HMSet(key, dict));
        }

        /// <summary>
        /// Set multiple hash fields to multiple values
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="keyValues">Array of [key,value,key,value,..]</param>
        /// <returns>Status code</returns>
        public Task<string> HMSetAsync(string key, params string[] keyValues)
        {
            return WriteAsync(RedisCommands.HMSet(key, keyValues));
        }

        /// <summary>
        /// Set the value of a hash field
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="field">Hash field to set</param>
        /// <param name="value">Value to set</param>
        /// <returns>True if field is new</returns>
        public Task<bool> HSetAsync(string key, string field, object value)
        {
            return WriteAsync(RedisCommands.HSet(key, field, value));
        }

        /// <summary>
        /// Set the value of a hash field, only if the field does not exist
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <param name="field">Hash field to set</param>
        /// <param name="value">Value to set</param>
        /// <returns>True if field was set to value</returns>
        public Task<bool> HSetNxAsync(string key, string field, object value)
        {
            return WriteAsync(RedisCommands.HSetNx(key, field, value));
        }

        /// <summary>
        /// Get all the values in a hash
        /// </summary>
        /// <param name="key">Hash key</param>
        /// <returns>Array of all values in hash</returns>
        public Task<string[]> HValsAsync(string key)
        {
            return WriteAsync(RedisCommands.HVals(key));
        }

        #endregion

        #region Lists

        /// <summary>
        /// Get an element from a list by its index
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="index">Zero-based index of item to return</param>
        /// <returns>Element at index</returns>
        public Task<string> LIndexAsync(string key, long index)
        {
            return WriteAsync(RedisCommands.LIndex(key, index));
        }

        /// <summary>
        /// Insert an element before or after another element in a list
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="insertType">Relative position</param>
        /// <param name="pivot">Relative element</param>
        /// <param name="value">Element to insert</param>
        /// <returns>Length of list after insert or -1 if pivot not found</returns>
        public Task<long> LInsertAsync(string key, RedisInsert insertType, string pivot, object value)
        {
            return WriteAsync(RedisCommands.LInsert(key, insertType, pivot, value));
        }

        /// <summary>
        /// Get the length of a list
        /// </summary>
        /// <param name="key">List key</param>
        /// <returns>Length of list at key</returns>
        public Task<long> LLenAsync(string key)
        {
            return WriteAsync(RedisCommands.LLen(key));
        }

        /// <summary>
        /// Remove and get the first element in a list
        /// </summary>
        /// <param name="key">List key</param>
        /// <returns>First element in list</returns>
        public Task<string> LPopAsync(string key)
        {
            return WriteAsync(RedisCommands.LPop(key));
        }

        /// <summary>
        /// Prepend one or multiple values to a list
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="values">Values to push</param>
        /// <returns>Length of list after push</returns>
        public Task<long> LPushAsync(string key, params object[] values)
        {
            return WriteAsync(RedisCommands.LPush(key, values));
        }

        /// <summary>
        /// Prepend a value to a list, only if the list exists
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="value">Value to push</param>
        /// <returns>Length of list after push</returns>
        public Task<long> LPushXAsync(string key, object value)
        {
            return WriteAsync(RedisCommands.LPushX(key, value));
        }

        /// <summary>
        /// Get a range of elements from a list
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="start">Start offset</param>
        /// <param name="stop">Stop offset</param>
        /// <returns>List of elements in range</returns>
        public Task<string[]> LRangeAsync(string key, long start, long stop)
        {
            return WriteAsync(RedisCommands.LRange(key, start, stop));
        }

        /// <summary>
        /// Remove elements from a list
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="count">&gt;0: remove N elements from head to tail; &lt;0: remove N elements from tail to head; =0: remove all elements</param>
        /// <param name="value">Remove elements equal to value</param>
        /// <returns>Number of removed elements</returns>
        public Task<long> LRemAsync(string key, long count, object value)
        {
            return WriteAsync(RedisCommands.LRem(key, count, value));
        }

        /// <summary>
        /// Set the value of an element in a list by its index
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="index">List index to modify</param>
        /// <param name="value">New element value</param>
        /// <returns>Status code</returns>
        public Task<string> LSetAsync(string key, long index, object value)
        {
            return WriteAsync(RedisCommands.LSet(key, index, value));
        }

        /// <summary>
        /// Trim a list to the specified range
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="start">Zero-based start index</param>
        /// <param name="stop">Zero-based stop index</param>
        /// <returns>Status code</returns>
        public Task<string> LTrimAsync(string key, long start, long stop)
        {
            return WriteAsync(RedisCommands.LTrim(key, start, stop));
        }

        /// <summary>
        /// Remove and get the last elment in a list
        /// </summary>
        /// <param name="key">List key</param>
        /// <returns>Value of last list element</returns>
        public Task<string> RPopAsync(string key)
        {
            return WriteAsync(RedisCommands.RPop(key));
        }

        /// <summary>
        /// Remove the last elment in a list, append it to another list and return it
        /// </summary>
        /// <param name="source">List source key</param>
        /// <param name="destination">Destination key</param>
        /// <returns>Element being popped and pushed</returns>
        public Task<string> RPopLPushAsync(string source, string destination)
        {
            return WriteAsync(RedisCommands.RPopLPush(source, destination));
        }

        /// <summary>
        /// Append one or multiple values to a list
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="values">Values to push</param>
        /// <returns>Length of list after push</returns>
        public Task<long> RPushAsync(string key, params object[] values)
        {
            return WriteAsync(RedisCommands.RPush(key, values));
        }

        /// <summary>
        /// Append a value to a list, only if the list exists
        /// </summary>
        /// <param name="key">List key</param>
        /// <param name="values">Values to push</param>
        /// <returns>Length of list after push</returns>
        public Task<long> RPushXAsync(string key, params object[] values)
        {
            return WriteAsync(RedisCommands.RPushX(key, values));
        }

        #endregion

        #region Sets

        /// <summary>
        /// Add one or more members to a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="members">Members to add to set</param>
        /// <returns>Number of elements added to set</returns>
        public Task<long> SAddAsync(string key, params object[] members)
        {
            return WriteAsync(RedisCommands.SAdd(key, members));
        }

        /// <summary>
        /// Get the number of members in a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <returns>Number of elements in set</returns>
        public Task<long> SCardAsync(string key)
        {
            return WriteAsync(RedisCommands.SCard(key));
        }

        /// <summary>
        /// Subtract multiple sets
        /// </summary>
        /// <param name="keys">Set keys to subtract</param>
        /// <returns>Array of elements in resulting set</returns>
        public Task<string[]> SDiffAsync(params string[] keys)
        {
            return WriteAsync(RedisCommands.SDiff(keys));
        }

        /// <summary>
        /// Subtract multiple sets and store the resulting set in a key
        /// </summary>
        /// <param name="destination">Destination key</param>
        /// <param name="keys">Set keys to subtract</param>
        /// <returns>Number of elements in the resulting set</returns>
        public Task<long> SDiffStoreAsync(string destination, params string[] keys)
        {
            return WriteAsync(RedisCommands.SDiffStore(destination, keys));
        }

        /// <summary>
        /// Intersect multiple sets
        /// </summary>
        /// <param name="keys">Set keys to intersect</param>
        /// <returns>Array of elements in resulting set</returns>
        public Task<string[]> SInterAsync(params string[] keys)
        {
            return WriteAsync(RedisCommands.SInter(keys));
        }

        /// <summary>
        /// Intersect multiple sets and store the resulting set in a key
        /// </summary>
        /// <param name="destination">Destination key</param>
        /// <param name="keys">Set keys to intersect</param>
        /// <returns>Number of elements in resulting set</returns>
        public Task<long> SInterStoreAsync(string destination, params string[] keys)
        {
            return WriteAsync(RedisCommands.SInterStore(destination, keys));
        }

        /// <summary>
        /// Determine if a given value is a member of a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="member">Member to lookup</param>
        /// <returns>True if member exists in set</returns>
        public Task<bool> SIsMemberAsync(string key, object member)
        {
            return WriteAsync(RedisCommands.SIsMember(key, member));
        }

        /// <summary>
        /// Get all the members in a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <returns>All elements in the set</returns>
        public Task<string[]> SMembersAsync(string key)
        {
            return WriteAsync(RedisCommands.SMembers(key));
        }

        /// <summary>
        /// Move a member from one set to another
        /// </summary>
        /// <param name="source">Source key</param>
        /// <param name="destination">Destination key</param>
        /// <param name="member">Member to move</param>
        /// <returns>True if element was moved</returns>
        public Task<bool> SMoveAsync(string source, string destination, object member)
        {
            return WriteAsync(RedisCommands.SMove(source, destination, member));
        }

        /// <summary>
        /// Remove and return a random member from a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <returns>The removed element</returns>
        public Task<string> SPopAsync(string key)
        {
            return WriteAsync(RedisCommands.SPop(key));
        }

        /// <summary>
        /// Get a random member from a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <returns>One random element from set</returns>
        public Task<string> SRandMemberAsync(string key)
        {
            return WriteAsync(RedisCommands.SRandMember(key));
        }

        /// <summary>
        /// Get one or more random members from a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="count">Number of elements to return</param>
        /// <returns>One or more random elements from set</returns>
        public Task<string[]> SRandMemberAsync(string key, long count)
        {
            return WriteAsync(RedisCommands.SRandMember(key, count));
        }

        /// <summary>
        /// Remove one or more members from a set
        /// </summary>
        /// <param name="key">Set key</param>
        /// <param name="members">Set members to remove</param>
        /// <returns>Number of elements removed from set</returns>
        public Task<long> SRemAsync(string key, params object[] members)
        {
            return WriteAsync(RedisCommands.SRem(key, members));
        }

        /// <summary>
        /// Add multiple sets
        /// </summary>
        /// <param name="keys">Set keys to union</param>
        /// <returns>Array of elements in resulting set</returns>
        public Task<string[]> SUnionAsync(params string[] keys)
        {
            return WriteAsync(RedisCommands.SUnion(keys));
        }

        /// <summary>
        /// Add multiple sets and store the resulting set in a key
        /// </summary>
        /// <param name="destination">Destination key</param>
        /// <param name="keys">Set keys to union</param>
        /// <returns>Number of elements in resulting set</returns>
        public Task<long> SUnionStoreAsync(string destination, params string[] keys)
        {
            return WriteAsync(RedisCommands.SUnionStore(destination, keys));
        }

        #endregion

        #region Strings

        /// <summary>
        /// Append a value to a key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to append to key</param>
        /// <returns>Length of string after append</returns>
        public Task<long> AppendAsync(string key, object value)
        {
            return WriteAsync(RedisCommands.Append(key, value));
        }

        /// <summary>
        /// Count set bits in a string
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <param name="start">Start offset</param>
        /// <param name="end">Stop offset</param>
        /// <returns>Number of bits set to 1</returns>
        public Task<long> BitCountAsync(string key, long? start = null, long? end = null)
        {
            return WriteAsync(RedisCommands.BitCount(key, start, end));
        }

        /// <summary>
        /// Perform bitwise operations between strings
        /// </summary>
        /// <param name="operation">Bit command to execute</param>
        /// <param name="destKey">Store result in destination key</param>
        /// <param name="keys">Keys to operate</param>
        /// <returns>Size of string stored in the destination key</returns>
        public Task<long> BitOpAsync(RedisBitOp operation, string destKey, params string[] keys)
        {
            return WriteAsync(RedisCommands.BitOp(operation, destKey, keys));
        }

        /// <summary>
        /// Find first bit set or clear in a string
        /// </summary>
        /// <param name="key">Key to examine</param>
        /// <param name="bit">Bit value (1 or 0)</param>
        /// <param name="start">Examine string at specified byte offset</param>
        /// <param name="end">Examine string to specified byte offset</param>
        /// <returns>Position of the first bit set to the specified value</returns>
        public Task<long> BitPosAsync(string key, bool bit, long? start = null, long? end = null)
        {
            return WriteAsync(RedisCommands.BitPos(key, bit, start, end));
        }

        /// <summary>
        /// Decrement the integer value of a key by one
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <returns>Value of key after decrement</returns>
        public Task<long> DecrAsync(string key)
        {
            return WriteAsync(RedisCommands.Decr(key));
        }

        /// <summary>
        /// Decrement the integer value of a key by the given number
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="decrement">Decrement value</param>
        /// <returns>Value of key after decrement</returns>
        public Task<long> DecrByAsync(string key, long decrement)
        {
            return WriteAsync(RedisCommands.DecrBy(key, decrement));
        }

        /// <summary>
        /// Get the value of a key
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <returns>Value of key</returns>
        public Task<string> GetAsync(string key)
        {
            return WriteAsync(RedisCommands.Get(key));
        }

        /// <summary>
        /// Returns the bit value at offset in the string value stored at key
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <param name="offset">Offset of key to check</param>
        /// <returns>Bit value stored at offset</returns>
        public Task<bool> GetBitAsync(string key, uint offset)
        {
            return WriteAsync(RedisCommands.GetBit(key, offset));
        }

        /// <summary>
        /// Get a substring of the string stored at a key
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <param name="start">Start offset</param>
        /// <param name="end">End offset</param>
        /// <returns>Substring in the specified range</returns>
        public Task<string> GetRangeAsync(string key, long start, long end)
        {
            return WriteAsync(RedisCommands.GetRange(key, start, end));
        }

        /// <summary>
        /// Set the string value of a key and return its old value
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <returns>Old value stored at key, or null if key did not exist</returns>
        public Task<string> GetSetAsync(string key, object value)
        {
            return WriteAsync(RedisCommands.GetSet(key, value));
        }

        /// <summary>
        /// Increment the integer value of a key by one
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <returns>Value of key after increment</returns>
        public Task<long> IncrAsync(string key)
        {
            return WriteAsync(RedisCommands.Incr(key));
        }

        /// <summary>
        /// Increment the integer value of a key by the given amount
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="increment">Increment amount</param>
        /// <returns>Value of key after increment</returns>
        public Task<long> IncrByAsync(string key, long increment)
        {
            return WriteAsync(RedisCommands.IncrBy(key, increment));
        }

        /// <summary>
        /// Increment the float value of a key by the given amount
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="increment">Increment amount</param>
        /// <returns>Value of key after increment</returns>
        public Task<double> IncrByFloatAsync(string key, double increment)
        {
            return WriteAsync(RedisCommands.IncrByFloat(key, increment));
        }

        /// <summary>
        /// Get the values of all the given keys
        /// </summary>
        /// <param name="keys">Keys to lookup</param>
        /// <returns>Array of values at the specified keys</returns>
        public Task<string[]> MGetAsync(params string[] keys)
        {
            return WriteAsync(RedisCommands.MGet(keys));
        }

        /// <summary>
        /// Set multiple keys to multiple values
        /// </summary>
        /// <param name="keyValues">Key values to set</param>
        /// <returns>Status code</returns>
        public Task<string> MSetAsync(params Tuple<string, string>[] keyValues)
        {
            return WriteAsync(RedisCommands.MSet(keyValues));
        }

        /// <summary>
        /// Set multiple keys to multiple values
        /// </summary>
        /// <param name="keyValues">Key values to set [k1, v1, k2, v2, ..]</param>
        /// <returns>Status code</returns>
        public Task<string> MSetAsync(params string[] keyValues)
        {
            return WriteAsync(RedisCommands.MSet(keyValues));
        }

        /// <summary>
        /// Set multiple keys to multiple values, only if none of the keys exist
        /// </summary>
        /// <param name="keyValues">Key values to set</param>
        /// <returns>True if all keys were set</returns>
        public Task<bool> MSetNxAsync(params Tuple<string, string>[] keyValues)
        {
            return WriteAsync(RedisCommands.MSetNx(keyValues));
        }

        /// <summary>
        /// Set multiple keys to multiple values, only if none of the keys exist
        /// </summary>
        /// <param name="keyValues">Key values to set [k1, v1, k2, v2, ..]</param>
        /// <returns>True if all keys were set</returns>
        public Task<bool> MSetNxAsync(params string[] keyValues)
        {
            return WriteAsync(RedisCommands.MSetNx(keyValues));
        }

        /// <summary>
        /// Set the value and expiration in milliseconds of a key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="milliseconds">Expiration in milliseconds</param>
        /// <param name="value">Value to set</param>
        /// <returns>Status code</returns>
        public Task<string> PSetExAsync(string key, long milliseconds, object value)
        {
            return WriteAsync(RedisCommands.PSetEx(key, milliseconds, value));
        }

        /// <summary>
        /// Set the string value of a key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <returns>Status code</returns>
        public Task<string> SetAsync(string key, object value)
        {
            return WriteAsync(RedisCommands.Set(key, value));
        }

        /// <summary>
        /// Set the string value of a key with atomic expiration and existence condition
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <param name="expiration">Set expiration to nearest millisecond</param>
        /// <param name="condition">Set key if existence condition</param>
        /// <returns>Status code, or null if condition not met</returns>
        public Task<string> SetAsync(string key, object value, TimeSpan expiration, RedisExistence? condition = null)
        {
            return WriteAsync(RedisCommands.Set(key, value, expiration, condition));
        }

        /// <summary>
        /// Set the string value of a key with atomic expiration and existence condition
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <param name="expirationSeconds">Set expiration to nearest second</param>
        /// <param name="condition">Set key if existence condition</param>
        /// <returns>Status code, or null if condition not met</returns>
        public Task<string> SetAsync(string key, object value, int? expirationSeconds = null, RedisExistence? condition = null)
        {
            return WriteAsync(RedisCommands.Set(key, value, expirationSeconds, condition));
        }

        /// <summary>
        /// Set the string value of a key with atomic expiration and existence condition
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <param name="expirationMilliseconds">Set expiration to nearest millisecond</param>
        /// <param name="condition">Set key if existence condition</param>
        /// <returns>Status code, or null if condition not met</returns>
        public Task<string> SetAsync(string key, object value, long? expirationMilliseconds = null, RedisExistence? condition = null)
        {
            return WriteAsync(RedisCommands.Set(key, value, expirationMilliseconds, condition));
        }

        /// <summary>
        /// Sets or clears the bit at offset in the string value stored at key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="offset">Modify key at offset</param>
        /// <param name="value">Value to set (on or off)</param>
        /// <returns>Original bit stored at offset</returns>
        public Task<bool> SetBitAsync(string key, uint offset, bool value)
        {
            return WriteAsync(RedisCommands.SetBit(key, offset, value));
        }

        /// <summary>
        /// Set the value and expiration of a key
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="seconds">Expiration in seconds</param>
        /// <param name="value">Value to set</param>
        /// <returns>Status code</returns>
        public Task<string> SetExAsync(string key, long seconds, object value)
        {
            return WriteAsync(RedisCommands.SetEx(key, seconds, value));
        }

        /// <summary>
        /// Set the value of a key, only if the key does not exist
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="value">Value to set</param>
        /// <returns>True if key was set</returns>
        public Task<bool> SetNxAsync(string key, object value)
        {
            return WriteAsync(RedisCommands.SetNx(key, value));
        }

        /// <summary>
        /// Overwrite part of a string at key starting at the specified offset
        /// </summary>
        /// <param name="key">Key to modify</param>
        /// <param name="offset">Start offset</param>
        /// <param name="value">Value to write at offset</param>
        /// <returns>Length of string after operation</returns>
        public Task<long> SetRangeAsync(string key, uint offset, object value)
        {
            return WriteAsync(RedisCommands.SetRange(key, offset, value));
        }

        /// <summary>
        /// Get the length of the value stored in a key
        /// </summary>
        /// <param name="key">Key to lookup</param>
        /// <returns>Length of string at key</returns>
        public Task<long> StrLenAsync(string key)
        {
            return WriteAsync(RedisCommands.StrLen(key));
        }

        #endregion

        #region Server

        /// <summary>
        /// Asyncronously rewrite the append-only file
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> BgRewriteAofAsync()
        {
            return WriteAsync(RedisCommands.BgRewriteAof());
        }

        /// <summary>
        /// Asynchronously save the dataset to disk
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> BgSaveAsync()
        {
            return WriteAsync(RedisCommands.BgSave());
        }

        /// <summary>
        /// Get the current connection name
        /// </summary>
        /// <returns>Connection name</returns>
        public Task<string> ClientGetNameAsync()
        {
            return WriteAsync(RedisCommands.ClientGetName());
        }

        /// <summary>
        /// Kill the connection of a client
        /// </summary>
        /// <param name="ip">Client IP returned from CLIENT LIST</param>
        /// <param name="port">Client port returned from CLIENT LIST</param>
        /// <returns>Status code</returns>
        public Task<string> ClientKillAsync(string ip, int port)
        {
            return WriteAsync(RedisCommands.ClientKill(ip, port));
        }

        /// <summary>
        /// Kill the connection of a client
        /// </summary>
        /// <param name="addr">Client address</param>
        /// <param name="id">Client ID</param>
        /// <param name="type">Client type</param>
        /// <param name="skipMe">Set to true to skip calling client</param>
        /// <returns>The number of clients killed</returns>
        public Task<long> ClientKillAsync(string addr = null, string id = null, string type = null, bool? skipMe = null)
        {
            return WriteAsync(RedisCommands.ClientKill(addr, id, type, skipMe));
        }

        /// <summary>
        /// Get the list of client connections
        /// </summary>
        /// <returns>Formatted string of clients</returns>
        public Task<string> ClientListAsync()
        {
            return WriteAsync(RedisCommands.ClientList());
        }

        /// <summary>
        /// Suspend all the Redis clients for the specified amount of time 
        /// </summary>
        /// <param name="milliseconds">Time in milliseconds to suspend</param>
        /// <returns>Status code</returns>
        public Task<string> ClientPauseAsync(int milliseconds)
        {
            return WriteAsync(RedisCommands.ClientPause(milliseconds));
        }

        /// <summary>
        /// Suspend all the Redis clients for the specified amount of time 
        /// </summary>
        /// <param name="timeout">Time to suspend</param>
        /// <returns>Status code</returns>
        public Task<string> ClientPauseAsync(TimeSpan timeout)
        {
            return WriteAsync(RedisCommands.ClientPause(timeout));
        }

        /// <summary>
        /// Set the current connection name
        /// </summary>
        /// <param name="connectionName">Name of connection (no spaces)</param>
        /// <returns>Status code</returns>
        public Task<string> ClientSetNameAsync(string connectionName)
        {
            return WriteAsync(RedisCommands.ClientSetName(connectionName));
        }

        /// <summary>
        /// Get the value of a configuration paramter
        /// </summary>
        /// <param name="parameter">Configuration parameter to lookup</param>
        /// <returns>Configuration value</returns>
        public Task<Tuple<string, string>[]> ConfigGetAsync(string parameter)
        {
            return WriteAsync(RedisCommands.ConfigGet(parameter));
        }

        /// <summary>
        /// Reset the stats returned by INFO
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> ConfigResetStatAsync()
        {
            return WriteAsync(RedisCommands.ConfigResetStat());
        }

        /// <summary>
        /// Rewrites the redis.conf file
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> ConfigRewriteAsync()
        {
            return WriteAsync(RedisCommands.ConfigRewrite());
        }

        /// <summary>
        /// Set a configuration parameter to the given value
        /// </summary>
        /// <param name="parameter">Parameter to set</param>
        /// <param name="value">Value to set</param>
        /// <returns>Status code</returns>
        public Task<string> ConfigSetAsync(string parameter, string value)
        {
            return WriteAsync(RedisCommands.ConfigSet(parameter, value));
        }

        /// <summary>
        /// Return the number of keys in the selected database
        /// </summary>
        /// <returns>Number of keys</returns>
        public Task<long> DbSizeAsync()
        {
            return WriteAsync(RedisCommands.DbSize());
        }

        /// <summary>
        /// Make the server crash :(
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> DebugSegFaultAsync()
        {
            return WriteAsync(RedisCommands.DebugSegFault());
        }

        /// <summary>
        /// Remove all keys from all databases
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> FlushAllAsync()
        {
            return WriteAsync(RedisCommands.FlushAll());
        }

        /// <summary>
        /// Remove all keys from the current database
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> FlushDbAsync()
        {
            return WriteAsync(RedisCommands.FlushDb());
        }

        /// <summary>
        /// Get information and statistics about the server
        /// </summary>
        /// <param name="section">all|default|server|clients|memory|persistence|stats|replication|cpu|commandstats|cluster|keyspace</param>
        /// <returns>Formatted string</returns>
        public Task<string> InfoAsync(string section = null)
        {
            return WriteAsync(RedisCommands.Info());
        }

        /// <summary>
        /// Get the timestamp of the last successful save to disk
        /// </summary>
        /// <returns>Date of last save</returns>
        public Task<DateTime> LastSaveAsync()
        {
            return WriteAsync(RedisCommands.LastSave());
        }

        /// <summary>
        /// Provide information on the role of a Redis instance in the context of replication
        /// </summary>
        /// <returns>Role information</returns>
        public Task<RedisRole> RoleAsync()
        {
            return WriteAsync(RedisCommands.Role());
        }

        /// <summary>
        /// Syncronously save the dataset to disk
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> SaveAsync()
        {
            return WriteAsync(RedisCommands.Save());
        }

        /// <summary>
        /// Syncronously save the dataset to disk an then shut down the server
        /// </summary>
        /// <param name="save">Force a DB saving operation even if no save points are configured</param>
        /// <returns>Status code</returns>
        public Task<string> ShutdownAsync(bool? save = null)
        {
            return WriteAsync(RedisCommands.Shutdown());
        }

        /// <summary>
        /// Make the server a slave of another instance or promote it as master
        /// </summary>
        /// <param name="host">Master host</param>
        /// <param name="port">master port</param>
        /// <returns>Status code</returns>
        public Task<string> SlaveOfAsync(string host, int port)
        {
            return WriteAsync(RedisCommands.SlaveOf(host, port));
        }

        /// <summary>
        /// Turn off replication, turning the Redis server into a master
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> SlaveOfNoOneAsync()
        {
            return WriteAsync(RedisCommands.SlaveOfNoOne());
        }

        /// <summary>
        /// Get latest entries from the slow log
        /// </summary>
        /// <param name="count">Limit entries returned</param>
        /// <returns>Slow log entries</returns>
        public Task<RedisSlowLogEntry[]> SlowLogGetAsync(long? count = null)
        {
            return WriteAsync(RedisCommands.SlowLogGet(count));
        }

        /// <summary>
        /// Get the length of the slow log
        /// </summary>
        /// <returns>Slow log length</returns>
        public Task<long> SlowLogLenAsync()
        {
            return WriteAsync(RedisCommands.SlowLogLen());
        }

        /// <summary>
        /// Reset the slow log
        /// </summary>
        /// <returns>Status code</returns>
        public Task<string> SlowLogResetAsync()
        {
            return WriteAsync(RedisCommands.SlowLogReset());
        }

        /// <summary>
        /// Internal command used for replication
        /// </summary>
        /// <returns>Byte array of Redis sync data</returns>
        public Task<byte[]> SyncAsync()
        {
            return WriteAsync(RedisCommands.Sync());
        }

        /// <summary>
        /// Return the current server time
        /// </summary>
        /// <returns>Server time</returns>
        public Task<DateTime> TimeAsync()
        {
            return WriteAsync(RedisCommands.Time());
        }

        #endregion
    }
}
