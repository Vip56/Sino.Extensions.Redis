namespace Sino.Extensions.Redis
{
    public class RedisMasterState
    {
        readonly long _downState;
        readonly string _leader;
        readonly long _voteEpoch;

        /// <summary>
        /// Get the master down state
        /// </summary>
        public long DownState { get { return _downState; } }

        /// <summary>
        /// Get the leader
        /// </summary>
        public string Leader { get { return _leader; } }

        /// <summary>
        /// Get the vote epoch
        /// </summary>
        public long VoteEpoch { get { return _voteEpoch; } }

        internal RedisMasterState(long downState, string leader, long voteEpoch)
        {
            _downState = downState;
            _leader = leader;
            _voteEpoch = voteEpoch;
        }
    }
}
