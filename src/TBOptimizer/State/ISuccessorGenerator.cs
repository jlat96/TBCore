using System;
using System.Collections.Generic;
using System.Text;

namespace TrailBlazer.TBOptimizer.State
{
    /// <summary>
    /// Generator for creating a neighborhood of states from a given state
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluable"></typeparam>
    public interface ISuccessorGenerator<TState, TEvaluable>
        where TState : IEvaluable<TEvaluable>
        where TEvaluable : IComparable<TEvaluable>
    {
        /// <summary>
        /// Returns the neighborhood of successor states for a given state
        /// </summary>
        /// <param name="state">The state for which to generate successors</param>
        /// <returns></returns>
        IEnumerable<TState> GetSuccessors(TState state);
    }
}
