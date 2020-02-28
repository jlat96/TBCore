using System;
namespace TBOptimizer.Climber.Events
{
    public class ClimberCompleteEvent<TState, TEvaluation> : EventArgs
    {
        public int CLimberIndex { get; set; }
        public int TotalStepsPerformed { get; set; }
        public TState InitialState { get; set; }
        public TState OptimizedState { get; set; }

        public override string ToString()
        {
            return $"Climber: {CLimberIndex}, Steps Completed: {TotalStepsPerformed}\nOptimized State:\n{OptimizedState.ToString()}";
        }
    }
}
