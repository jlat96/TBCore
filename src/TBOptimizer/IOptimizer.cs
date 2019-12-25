using System;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer
{
    public interface IOptimizer<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Performs an optimization from an initial state. Will produce the most optimal configuration available from the performed operation.
        /// </summary>
        /// <param name="initialState">The initial state from which to optimize</param>
        /// <returns>The most optimal state as determined by the hill climbing search</returns>
        TState PerformOptimization(TState initialState);
    }
}
