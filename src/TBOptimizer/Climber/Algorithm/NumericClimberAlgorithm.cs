using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber.Algorithm
{
    /// <summary>
    /// A climber algorithm that implements a basic local search using integer evaluation. Optimize will return the most optimal state that is
    /// encountered before no state with a better evaluation is generated.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public class NumericClimberAlgorithm<TState> : LocalClimberAlgorithm<TState, int>
        where TState : EvaluableState<TState, int>
    {
        /// <summary>
        /// Creates a new NumericClimberAlgorithm using the given comparison strategy to compare
        /// discrete evaluations and the given successorPicker to select the next EvaluableState to
        /// evaluate against non-greedily
        /// </summary>
        /// <param name="comparer">The Comparer that which Optimize will use to determine optimality</param>
        /// <param name="successorPicker">The successor picker to choose the next EvaluableState in to evaluate at an optimization step</param>
        public NumericClimberAlgorithm(IComparer<int> comparer, ISuccessorPicker<TState, int> successorPicker) : base(comparer, successorPicker) { }

        /// <summary>
        /// Creates a new LocalClimberAlgorithm using the given comparison strategy to compare
        /// discrete evaluations and the given successorPicker to select the next EvaluableState to
        /// evaluate against in a greedy or non-greedy configuration
        /// </summary>
        /// <param name="greedy">Determines the greediness of the algorithm</param>
        /// <param name="comperer">The Comparer that which Optimize will use to compare optimizations</param>
        /// <param name="successorPicker">The successor picker to choose the next EvaluableState in to evaluate at an optimization step</param>
        public NumericClimberAlgorithm(bool greedy, IComparer<int> comperer, ISuccessorPicker<TState, int> successorPicker) : base(greedy, comperer, successorPicker) { }
    }
}
