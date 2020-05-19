using System;
using System.Collections.Generic;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber
{
    /// <summary>
    /// IterableHillClimber is a classification of HillClimber that will iterate through several different HillClimber optimizations to produce a potentially more optimal result
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluation"></typeparam>
    public abstract class IterableHillClimber<TState, TEvaluation> : IHillClimber<TState, TEvaluation>, IIterableClimber<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Raises an event whenever a climber optimization is completed
        /// </summary>
        public EventHandler<ClimberCompleteEvent<TState, TEvaluation>> ClimberCompleteEvent { get; set; }

        /// <inheritdoc/>
        public EventHandler<ClimberStepEvent<TState, TEvaluation>> ClimberStepPerformedEvent { get; set; }

        public abstract TState Optimize(TState initialState);
    }
}
