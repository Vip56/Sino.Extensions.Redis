using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sino.CacheStore.Internal
{
    public class RedisCommandFactory : CommandFactory
    {
        public RedisCommandFactory()
            : base("Redis") { }

        #region Basic

        public override StatusCommand CreateAuthCommand(string token)
        {
            var cmd = new StatusCommand("AUTH", token);
            OnCommand(cmd);

            return cmd;
        }

        public override StatusCommand CreatePingCommand()
        {
            var cmd = new StatusCommand("PING");
            OnCommand(cmd);

            return cmd;
        }

        public override StatusCommand CreateQuitCommand()
        {
            var cmd = new StatusCommand("QUIT");
            OnCommand(cmd);

            return cmd;
        }

        public override StatusCommand CreateSelectCommand(string dbName)
        {
            var cmd = new StatusCommand("SELECT", int.Parse(dbName));
            OnCommand(cmd);

            return cmd;
        }

        #endregion

        #region Key

        public override BoolCommand CreateExistsCommand(string key)
        {
            var cmd = new BoolCommand("EXISTS", key);
            OnCommand(cmd);

            return cmd;
        }

        public override BytesCommand CreateGetCommand(string key)
        {
            var cmd = new BytesCommand("GET", key);
            OnCommand(cmd);

            return cmd;
        }

        public override StatusCommand CreateSetCommand(string key, object value, int? expirationSeconds = null, long? expirationMilliseconds = null, CacheStoreExistence? exists = null)
        {
            var args = new List<string> { key, value.ToString() };
            if (expirationSeconds != null)
                args.AddRange(new[] { "EX", expirationSeconds.ToString() });
            if (expirationMilliseconds != null)
                args.AddRange(new[] { "PX", expirationMilliseconds.ToString() });
            if (exists != null)
                args.AddRange(new[] { exists.ToString().ToUpperInvariant() });
            var cmd = new StatusCommand("SET", args.ToArray());
            cmd.IsNullable = true;
            OnCommand(cmd);

            return cmd;
        }

        public override BoolCommand CreateExpireCommand(string key, int seconds)
        {
            var cmd = new BoolCommand("EXPIRE", key, seconds);
            OnCommand(cmd);

            return cmd;
        }

        public override BoolCommand CreatePExpireCommand(string key, long milliseconds)
        {
            var cmd = new BoolCommand("PEXPIRE", key, milliseconds);
            OnCommand(cmd);

            return cmd;
        }

        public override IntCommand CreateRemoveCommand(params string[] keys)
        {
            var cmd = new IntCommand("DEL", keys);
            OnCommand(cmd);

            return cmd;
        }

        #endregion

        #region Hash

        public override IntCommand CreateHDelCommand(string key, params string[] fields)
        {
            var cmd = new IntCommand("HDEL", fields.Insert(key).ToArray());
            OnCommand(cmd);

            return cmd;
        }

        public override BoolCommand CreateHExistsCommand(string key, string field)
        {
            var cmd = new BoolCommand("HEXISTS", key, field);
            OnCommand(cmd);

            return cmd;
        }

        public override BytesCommand CreateHGetCommand(string key, string field)
        {
            var cmd = new BytesCommand("HGET", key, field);
            OnCommand(cmd);

            return cmd;
        }

        public override IntCommand CreateHLenCommand(string key)
        {
            var cmd = new IntCommand("HLEN", key);
            OnCommand(cmd);

            return cmd;
        }

        public override BoolCommand CreateHSetCommand(string key, string field, byte[] value)
        {
            var cmd = new BoolCommand("HSET", key, field, value);
            OnCommand(cmd);

            return cmd;
        }

        public override BoolCommand CreateHSetWithNoExistCommand(string key, string field, byte[] value)
        {
            var cmd = new BoolCommand("HSETNX", key, field, value);
            OnCommand(cmd);

            return cmd;
        }

        #endregion

        #region List

        public override BytesCommand CreateLPopCommand(string key)
        {
            var cmd = new BytesCommand("LPOP", key);
            OnCommand(cmd);

            return cmd;
        }

        public override BytesCommand CreateLIndexCommand(string key, long index)
        {
            var cmd = new BytesCommand("LINDEX", key, index);
            OnCommand(cmd);

            return cmd;
        }

        public override IntCommand CreateLLenCommand(string key)
        {
            var cmd = new IntCommand("LLEN", key);
            OnCommand(cmd);

            return cmd;
        }

        public override IntCommand CreateLPushCommand(string key, params object[] values)
        {
            var cmd = new IntCommand("LPUSH", values.Insert(key).ToArray());
            OnCommand(cmd);

            return cmd;
        }

        public override BytesCommand CreateRPopCommand(string key)
        {
            var cmd = new BytesCommand("RPOP", key);
            OnCommand(cmd);

            return cmd;
        }

        public override IntCommand CreateRPushCommand(string key, params object[] values)
        {
            var cmd = new IntCommand("RPUSH", values.Insert(key).ToArray());
            OnCommand(cmd);

            return cmd;
        }

        #endregion

        #region BitAndNumber

        public override IntCommand CreateBitCountCommand(string key, long? start = null, long? end = null)
        {
            string[] args = start.HasValue && end.HasValue
                ? new[] { key, start.Value.ToString(), end.Value.ToString() }:
                new[] { key };
            return new IntCommand("BITCOUNT", args);
        }

        public override BoolCommand CreateSetBitCommand(string key, uint offset, bool value)
        {
            return new BoolCommand("SETBIT", key, offset, value ? "1" : "0");
        }

        public override BoolCommand CreateGetBitCommand(string key, uint offset)
        {
            return new BoolCommand("GETBIT", key, offset);
        }

        public override IntCommand CreateDecrCommand(string key)
        {
            return new IntCommand("DECR", key);
        }

        public override IntCommand CreateDecrByCommand(string key, long decrement)
        {
            return new IntCommand("DECRBY", key, decrement);
        }

        public override IntCommand CreateIncrCommand(string key)
        {
            return new IntCommand("INCR", key);
        }

        public override IntCommand CreateIncrByCommand(string key, long increment)
        {
            return new IntCommand("INCRBY", key, increment);
        }

        #endregion
    }
}
