using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    public class GeneralHillClimber<TState> : HillClimber<TState, int>
        where TState : EvaluableState<TState, int>
    {
        public GeneralHillClimber(ClimberAlgorithm<TState, int> algorithm) : base(algorithm) { }
    }
}
