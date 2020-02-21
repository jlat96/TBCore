using System;
namespace TBOptimizer.Climber.Events
{
    public class ClimberRestartResultEvent<TState, TEvaluation> : EventArgs
    {
        public int RestartNumber { get; set; }
        public int TotalStepsPerformed { get; set; }
        public TState InitialState { get; set; }
        public TState OptimizedState { get; set; }

        public override string ToString()
        {
            return $"Restart: {RestartNumber}, Steps Completed: {TotalStepsPerformed}\nOptimized State:\n{OptimizedState.ToString()}";
        }
    }
}
