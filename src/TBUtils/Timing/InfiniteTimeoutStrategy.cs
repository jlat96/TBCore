using System;
namespace TBUtils.Timing
{
    public class InfiniteTimeoutStrategy : TimeoutStrategy
    {
        public InfiniteTimeoutStrategy()
        {
        }

        public override bool IsTimedOut()
        {
            return false;
        }
    }
}
