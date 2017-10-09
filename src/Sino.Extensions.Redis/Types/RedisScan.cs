namespace Sino.Extensions.Redis
{
    public class RedisScan<T>
    {
        readonly long _cursor;
        readonly T[] _items;

        /// <summary>
        /// Updated cursor that should be used as the cursor argument in the next call
        /// </summary>
        public long Cursor { get { return _cursor; } }

        /// <summary>
        /// Collection of elements returned by the SCAN operation
        /// </summary>
        public T[] Items { get { return _items; } }

        internal RedisScan(long cursor, T[] items)
        {
            _cursor = cursor;
            _items = items;
        }
    }
}
