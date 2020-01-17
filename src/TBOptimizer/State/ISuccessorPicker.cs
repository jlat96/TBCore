using System;

namespace TrailBlazer.TBOptimizer.State
{
    /// <summary>
    /// State selector for selecting a Next state based on a given initial state
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public interface ISuccessorPicker<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Returns the next successor state
        /// </summary>
        /// <param name="current">The successor for which to get the next state</param>
        /// <returns>The next state</returns>
        TState Next(TState current);
    }
}
