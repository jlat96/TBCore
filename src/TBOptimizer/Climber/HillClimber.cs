using System;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    /// <summary>
    /// HillClimber is an optimizer that will perform a local search hill climbing optimization on an initial state, continuing the
    /// operation until no more optimal state can be generated from a generated neighborhood.
    /// </summary>
    /// <typeparam name="TState">The type of the EvaluableState that is being evaluated</typeparam>
    /// <typeparam name="TEvaluation">The type of the Comparable result of an evaluation</typeparam>
    public abstract class HillClimber<TState, TEvaluation> : Optimizer<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected readonly IClimberAlgorithm<TState, TEvaluation> algorithm;

        /// <summary>
        /// Creates a HillClimber that will perform an optimization from the given ClimberAlgrithm.
        /// </summary>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        protected HillClimber(ClimberAlgorithm<TState, TEvaluation> algorithm) : base (algorithm.SuccessorPicker)
        {
            this.algorithm = algorithm;

        }

        /// <summary>
        /// Performs the climber optimization from a given initialState
        /// </summary>
        /// <param name="initialState">The initial state for which to optimize</param>
        /// <returns>The most optimal state encountered by the climber. This may not be the best possible state</returns>
        public override TState PerformOptimization(TState initialState)
        {
            return algorithm.Optimize(initialState);
        }
    }
}
