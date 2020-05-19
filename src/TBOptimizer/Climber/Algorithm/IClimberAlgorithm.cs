using System;
using System.Collections.Generic;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    /// <summary>
    /// Provides a base interface for a Hill Climbing optimization algorithm
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluation"></typeparam>
    public interface IClimberAlgorithm<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Performs a HillClimbing operation on a <see cref="TState"/> to yield the most optimal encountered state
        /// </summary>
        /// <param name="initialState">The initial state from which to optimize</param>
        /// <returns>The most optimal state encountered by the optimization operation. May be the initial state if
        /// no more optimal state could be found</returns>
        TState Optimize(TState initialState);

        ISuccessorSelector<TState, TEvaluation> SuccessorPicker { get; }

        /// <summary>
        /// Raises an event when a step of the climber optmimization is completed.
        /// </summary>
        EventHandler<ClimberStepEvent<TState, TEvaluation>> ClimbStepPerformedEvent { get; set; }
    }
}
