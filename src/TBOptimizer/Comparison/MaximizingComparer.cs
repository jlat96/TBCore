using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TrailBlazer.TBOptimizer.Comparison
{
    /// <summary>
    /// A comparer that will determine if the left side is greater than the right.
    /// </summary>
    /// <typeparam name="TState">The type of the state being compared</typeparam>
    public class MaximizingComparer<TState> : IComparer<TState>
        where TState : IComparable<TState>
    {
        /// <summary>
        /// Compares left to right to determine if left is greater than right.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Returns a positive integer if left > right, a negative integer if right > left, or 0 if left is equal to right</returns>
        public int Compare([AllowNull] TState left, [AllowNull] TState right)
        {
            return left.CompareTo(right) * -1;
        }
    }
}
