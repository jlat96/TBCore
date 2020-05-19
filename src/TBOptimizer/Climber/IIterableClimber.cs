using System;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber
{
    public interface IIterableClimber<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        public EventHandler<ClimberCompleteEvent<TState, TEvaluation>> ClimberCompleteEvent { get; set; }
    }
}
