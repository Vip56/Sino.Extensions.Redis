namespace Sino.CacheStore.Internal
{
    public class CacheStoreCommand
    {
        public string Command { get; }

        public object[] Arguments { get; }

        protected CacheStoreCommand(string command, params object[] args)
        {
            Command = command;
            Arguments = args;
        }
    }

    public abstract class CacheStoreCommand<T> : CacheStoreCommand
    {
        protected CacheStoreCommand(string command, params object[] args)
            : base(command, args) { }

        public T Result { get; set; }

        public override string ToString()
        {
            return $"{Command} {string.Join(" ", Arguments)}";
        }
    }
}
