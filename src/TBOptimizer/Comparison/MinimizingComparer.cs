using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TrailBlazer.TBOptimizer.Comparison
{
    public class MinimizingComparer<TState> : IComparer<TState>
        where TState : IComparable<TState>
    {
        /// <summary>
        /// Compares left to right to determine if left is less than right.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Returns a positive integer if "left" < right, a negative integer if right < left, or 0 if left is equal to right</returns>

        public int Compare([AllowNull] TState left, [AllowNull] TState right)
        {
            return left.CompareTo(right);
        }
    }
}
