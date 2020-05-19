using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer;
using TrailBlazer.TBOptimizer.State;

namespace Trailblazer.TBOptimizer.Randomizer
{
    /// <summary>
    /// Represents a monte-carlo state randomizer that will ingest an initial state and continually generate randomized
    /// states.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluation"></typeparam>
    public class StateRandomizer<TState, TEvaluation> : Optimizer<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Create a new StateRandomizer with the given compaer, using the given SuccessorPicker that will perform
        /// a single randomization and return the most optimal result
        /// </summary>
        /// <param name="evaluationComparer"></param>
        /// <param name="successorPicker"></param>
        public StateRandomizer(IComparer<TEvaluation> evaluationComparer, ISuccessorSelector<TState, TEvaluation> successorPicker)
            : this(evaluationComparer, successorPicker, 1) { }

        /// <summary>
        /// Create a new StateRandomizer with the given compaer, using the given SuccessorPicker that will perform
        /// the given number of randomizations before returning a result. A numRanfomizations of -1 will have the optimier
        /// return the first encountered optimal state
        /// </summary>
        /// <param name="evaluationComparer"></param>
        /// <param name="successorPicker"></param>
        /// <param name="numRandomizations"></param>
        public StateRandomizer(IComparer<TEvaluation> evaluationComparer, ISuccessorSelector<TState, TEvaluation> successorPicker, int numRandomizations)
        {
            SuccessorPicker = successorPicker;
            EvaluationComparer = evaluationComparer;
            NumRandomizations = numRandomizations;
        }

        /// <summary>
        /// The <see cref="IComparer{TEvaluation}">Comparer</see> used in state evaluation comparison
        /// </summary>
        internal IComparer<TEvaluation> EvaluationComparer { get; set; }

        /// <summary>
        /// The number of times that the optimizer will attempt to find a more optimal state.
        /// </summary>
        internal int NumRandomizations { get; set; } = 1;

        internal ISuccessorSelector<TState, TEvaluation> SuccessorPicker { get; set; }

        /// <summary>
        /// Performs a Monte Carlo optimization on the given initial state.
        /// </summary>
        /// <param name="initialState"></param>
        /// <returns>The most optimal state encountered after performing the specified evaluations</returns>
        public override TState Optimize(TState initialState)
        {
            if (initialState == null)
            {
                throw new ArgumentException("Initial state cannot be null");
            }

            TState bestState = initialState;
            TState nextState;
            for (int i = 0; i < NumRandomizations && bestState > initialState; i++) // Test this
            {
                nextState = SuccessorPicker.Next(bestState);
                bestState = EvaluationComparer.Compare(bestState.GetEvaluation(), nextState.GetEvaluation()) > 0
                    ? bestState
                    : nextState;
            }

            return bestState;
        }
    }
}
