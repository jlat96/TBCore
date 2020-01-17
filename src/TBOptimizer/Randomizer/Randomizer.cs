using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer;
using TrailBlazer.TBOptimizer.State;

namespace Trailblazer.TBOptimizer.Randomizer
{
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
        public StateRandomizer(IComparer<TEvaluation> evaluationComparer, ISuccessorPicker<TState, TEvaluation> successorPicker)
            : this(evaluationComparer, successorPicker, 1) { }

        /// <summary>
        /// Create a new StateRandomizer with the given compaer, using the given SuccessorPicker that will perform
        /// the given number of randomizations before returning a result. A numRanfomizations of -1 will have the optimier
        /// return the first encountered optimal state
        /// </summary>
        /// <param name="evaluationComparer"></param>
        /// <param name="successorPicker"></param>
        /// <param name="numRandomizations"></param>
        public StateRandomizer(IComparer<TEvaluation> evaluationComparer, ISuccessorPicker<TState, TEvaluation> successorPicker, int numRandomizations)
            : base (successorPicker)
        {
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

        /// <summary>
        /// Performs a Monte Carlo optimization on the given initial state.
        /// </summary>
        /// <param name="initialState"></param>
        /// <returns>The most optimal state encountered after performing the specified evaluations</returns>
        public override TState PerformOptimization(TState initialState)
        {
            if (initialState == null)
            {
                throw new ArgumentException("Initial state cannot be null");
            }

            TState bestState = initialState;
            TState nextState;
            for (int i = 0; i < NumRandomizations && bestState > initialState; i++) // Test this
            {
                nextState = successorPicker.Next(bestState);
                bestState = EvaluationComparer.Compare(bestState.GetEvaluation(), nextState.GetEvaluation()) > 0
                    ? bestState
                    : nextState;
            }

            return bestState;
        }
    }
}
