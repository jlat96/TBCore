using System;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer
{
    /// <summary>
    /// Provides a base for an implemention of an optimization operation
    /// </summary>
    /// <typeparam name="TState">The type of the state being optimized</typeparam>
    /// <typeparam name="TEvaluation">The type of the Evaluation of TState</typeparam>
    public interface IOptimizer<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Performs an optimization from an initial state. Will produce the most optimal configuration available from the performed operation.
        /// </summary>
        /// <param name="initialState">The initial state from which to optimize</param>
        /// <returns>The most optimal state as determined by the optimization implementation</returns>
        TState Optimize(TState initialState);
    }
}
