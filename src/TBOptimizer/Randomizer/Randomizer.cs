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
        private readonly IComparer<TEvaluation> evaluationComparer;
        private readonly int numRandomizations;

        public StateRandomizer(IComparer<TEvaluation> evaluationComparer, ISuccessorPicker<TState, TEvaluation> successorPicker, int numRandomizations)
            : base (successorPicker)
        {
            this.evaluationComparer = evaluationComparer;
            this.numRandomizations = numRandomizations;
        }

        public override TState PerformOptimization(TState initialState)
        {
            TState bestState = initialState;
            TState nextState;
            for (int i = 0; i < numRandomizations; i++)
            {
                nextState = successorPicker.Next(bestState);
                bestState = evaluationComparer.Compare(bestState.GetEvaluation(), nextState.GetEvaluation()) > 0
                    ? bestState
                    : nextState;
            }

            return bestState;
        }
    }
}
