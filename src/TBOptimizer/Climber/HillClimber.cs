using System;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    public abstract class HillClimber<TState, TEvaluation> : Optimizer<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected readonly IClimberAlgorithm<TState, TEvaluation> algorithm;

        protected HillClimber(ClimberAlgorithm<TState, TEvaluation> algorithm) : base (algorithm.GetSuccessorPicker())
        {
            this.algorithm = algorithm;

        }

        public override TState PerformOptimization(TState initialState)
        {
            return algorithm.Optimize(initialState);
        }
    }
}
