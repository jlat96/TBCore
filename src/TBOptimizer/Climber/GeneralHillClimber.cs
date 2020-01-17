using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    /// <summary>
    /// A basic HillClimber that evaluates TState using an integer evaluable type
    /// </summary>
    /// <typeparam name="TState">The type of the EvaluableState being optimized</typeparam>
    public class GeneralHillClimber<TState> : HillClimber<TState, int>
        where TState : EvaluableState<TState, int>
    {
        /// <summary>
        /// Creates a new GeneralHillClimber using the given ClimberAlgorithm for evaluation
        /// </summary>
        /// <param name="algorithm">The ClimberAlgorithm to use for optimization</param>
        public GeneralHillClimber(ClimberAlgorithm<TState, int> algorithm) : base(algorithm) { }
    }
}
