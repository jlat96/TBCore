using System;
namespace TBUtils.Timing
{
    public interface ITimeoutStrategy
    {
        public bool IsTimedOut();
    }
}
