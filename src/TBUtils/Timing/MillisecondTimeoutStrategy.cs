using System;
using System.Diagnostics;

namespace TBUtils.Timing
{
    /// <summary>
    /// Timeout based on a given number of milliseconds. Timeout clock will stop
    /// when the the first time IsTimeOut is queried before after the timeout is reached;
    /// </summary>
    public class MillisecondTimeoutStrategy : TimeoutStrategy
    {
        public long timeoutMilliseconds;
        public Stopwatch timer;
        public MillisecondTimeoutStrategy(long timeoutMilliseconds)
        {
            this.timeoutMilliseconds = timeoutMilliseconds;
        }

        private void Initialize()
        {
            timer = new Stopwatch();
        }

        public override bool IsTimedOut()
        {
            if (timer.ElapsedMilliseconds > timeoutMilliseconds)
            {
                Stop();
                return true;
            }

            return false;
        }

        public override void Reset()
        {
            Initialize();
        }

        public override void Start()
        {
            timer.Start();
        }

        public override void Stop()
        {
            timer.Stop();
        }
    }
}
