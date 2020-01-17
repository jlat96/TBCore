using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    /// <summary>
    /// A climber algorithm that implements a basic local search. Optimize will return the most optimal state that is
    /// encountered before no state with a better evaluation is generated.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluation"></typeparam>
    public class LocalClimberAlgorithm<TState, TEvaluation> : ClimberAlgorithm<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Creates a new LocalClimberAlgorithm using the given comparison strategy to compare
        /// evaluations and the given successorPicker to select the next EvaluableState to
        /// evaluate against non-greedily
        /// </summary>
        /// <param name="comparisonStrategy">The Comparer that which Optimize will use to determine optimality</param>
        /// <param name="successorPicker">The successor picker to choose the next EvaluableState in to evaluate at an optimization step</param>
        public LocalClimberAlgorithm(IComparer<TEvaluation> comparisonStrategy, ISuccessorPicker<TState, TEvaluation> successorPicker) : this(false, comparisonStrategy, successorPicker) { }

        /// <summary>
        /// Creates a new LocalClimberAlgorithm using the given comparison strategy to compare
        /// evaluations and the given successorPicker to select the next EvaluableState to
        /// evaluate against in a greedy or non-greedy configuration
        /// </summary>
        /// <param name="greedy">Determines the greedyness of the algorithm</param>
        /// <param name="comparisonStrategy">The Comparer that which Optimize will use to compare optimizations</param>
        /// <param name="successorPicker">The successor picker to choose the next EvaluableState in to evaluate at an optimization step</param>
        public LocalClimberAlgorithm(bool greedy, IComparer<TEvaluation> comparisonStrategy, ISuccessorPicker<TState, TEvaluation> successorPicker) : base(greedy, comparisonStrategy, successorPicker) { }

        /// <summary>
        /// Returns the most optimal state that is encountered before no state with a better evaluation is generated.
        /// </summary>
        /// <param name="initialState"></param>
        /// <returns></returns>
        public override TState Optimize(TState initialState)
        {
            TState currentState;
            TState nextState;

            currentState = initialState;

            while (true)
            {
                System.Diagnostics.Debug.Write(currentState);
                nextState = successorPicker.Next(currentState);

                if (currentState.GetEvaluation().Equals(nextState.GetEvaluation()))
                {
                    return currentState;
                }

                currentState = nextState;
            }
        }
    }
}
