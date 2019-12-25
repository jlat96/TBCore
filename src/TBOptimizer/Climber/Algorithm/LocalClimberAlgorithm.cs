using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    public class LocalClimberAlgorithm<TState, TEvaluation> : ClimberAlgorithm<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        public LocalClimberAlgorithm(IComparer<TEvaluation> comparisonStrategy, ISuccessorPicker<TState, TEvaluation> successorPicker) : this(false, comparisonStrategy, successorPicker) { }

        public LocalClimberAlgorithm(bool greedy, IComparer<TEvaluation> comparisonStrategy, ISuccessorPicker<TState, TEvaluation> successorPicker) : base(greedy, comparisonStrategy, successorPicker) { }

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
