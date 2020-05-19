using System;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber.Events
{
    /// <summary>
    /// Represents a step in a Climber Optimization operation.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluation"></typeparam>
    public class ClimberStepEvent<TState, TEvaluation> : EventArgs
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        public int StepsPerformed { get; set; } = 0;
        public TState CurrentState { get; set; }

        public override string ToString()
        {
            return $"Steps Completed: {StepsPerformed}\nCurrent State: {CurrentState.ToString()}";
        }
    }

    
}
