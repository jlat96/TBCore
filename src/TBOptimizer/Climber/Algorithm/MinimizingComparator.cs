using System;
using System.Collections.Generic;
using System.Text;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    public class MinimizingComparator : IComparer<int>
    {
        public int Compare(int left, int right)
        {
            return left.CompareTo(right) * -1;
        }
    }
}
