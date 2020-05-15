using System;
namespace TBUtils.Timing
{
    public abstract class TimeoutStrategy : ITimeoutStrategy
    {
        public abstract bool IsTimedOut();

        /// <summary>
        /// Resets the timeout. By default, Reset does nothing;
        /// </summary>
        public virtual void Reset()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Stop()
        {

        }

        public static TimeoutStrategy Infinite()
        {
            return new InfiniteTimeoutStrategy();
        }
    }
}
