using System;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber.Events
{
    public class ClimberStepEvent<TState, TEvaluation> : EventArgs
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        public int StepsPerformed { get; set; } = 0;
        public TState StepState { get; set; }

        public override string ToString()
        {
            return $"Steps Completed: {StepsPerformed}\nCurrent State: {StepState.ToString()}";
        }
    }

    
}
