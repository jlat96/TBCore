using System.Collections.Generic;
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
        /// Creates a new GeneralHillClimber using the given comparison strategy and successor generator for integer evaluation
        /// </summary>
        /// <param name="comparer">The comparison strategy to optimize with</param>
        /// <param name="successorGenerator">The successor genereator from which the best state will be selected</param>
        public GeneralHillClimber(IComparer<int> comparer, ISuccessorGenerator<TState, int> successorGenerator)
            : this(new LocalClimberAlgorithm<TState, int>(comparer, new ClimberSuccessorSelector<TState, int>(successorGenerator, comparer))) { }

        /// <summary>
        /// Creates a new GeneralHillClimber using the given ClimberAlgorithm for integer evaluation
        /// </summary>
        /// <param name="algorithm">The ClimberAlgorithm to use for optimization</param>
        public GeneralHillClimber(ClimberAlgorithm<TState, int> algorithm) : base(algorithm) { }
    }
}
