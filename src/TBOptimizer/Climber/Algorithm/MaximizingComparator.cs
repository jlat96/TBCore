using System;
using System.Collections.Generic;
using System.Text;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    public class MaximizingComparator : IComparer<int>
    {
        public int Compare(int left, int right)
        {
            return left.CompareTo(right);
        }
    }
}
