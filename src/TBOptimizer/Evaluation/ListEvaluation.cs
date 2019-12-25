using System;
using System.Collections.Generic;
using System.Linq;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Evaluation
{
    public static class ListEvaluation
    {
        public static TState Extrema<TState, TEvaluation>(this IEnumerable<TState> collection, IComparer<TEvaluation> comparer)
            where TState : IEvaluable<TEvaluation>
            where TEvaluation : IComparable<TEvaluation>
        {
            return collection.OrderBy(e => e.GetEvaluation(), comparer).First();
        }
    }
}
