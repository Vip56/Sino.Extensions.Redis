using System;

namespace Sino.Extensions.Redis
{
    public class RedisSlowLogEntry
    {
        readonly long _id;
        readonly DateTime _date;
        readonly TimeSpan _latency;
        readonly string[] _arguments;

        /// <summary>
        /// Get the entry ID
        /// </summary>
        public long Id { get { return _id; } }
        /// <summary>
        /// Get the entry date
        /// </summary>
        public DateTime Date { get { return _date; } }
        /// <summary>
        /// Get the entry latency
        /// </summary>
        public TimeSpan Latency { get { return _latency; } }
        /// <summary>
        /// Get the entry arguments
        /// </summary>
        public string[] Arguments { get { return _arguments; } }

        internal RedisSlowLogEntry(long id, DateTime date, TimeSpan latency, string[] arguments)
        {
            _id = id;
            _date = date;
            _latency = latency;
            _arguments = arguments;
        }
    }
}
