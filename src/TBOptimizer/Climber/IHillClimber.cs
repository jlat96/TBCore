using System;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber
{
    /// <summary>
    /// An abstraction of a Hill Climbing Optimizer. Iterably tests a state against
    /// its neighbors until no more optimal state can be reached.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluation"></typeparam>
    public interface IHillClimber<TState, TEvaluation> : IOptimizer<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Raises an event whenever the climber completes an optimization step
        /// </summary>
        EventHandler<ClimberStepEvent<TState, TEvaluation>> ClimberStepPerformedEvent { get; set; }
    }
}
