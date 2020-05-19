using System;
using System.Collections.Generic;
using TBOptimizer.Climber.Events;
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
        /// evaluate against
        /// </summary>
        /// <param name="successorPicker">The successor picker to choose the next EvaluableState in to evaluate at an optimization step</param>
        /// 
        public LocalClimberAlgorithm(ISuccessorSelector<TState, TEvaluation> successorPicker) : base(successorPicker) { }

        /// <summary>
        /// Returns the most optimal state that is encountered before no state with a better evaluation is generated.
        /// May return the initial state if no more optimal state could be found
        /// </summary>
        /// <param name="initialState">The initial state from which to optimize</param>
        /// <returns>The most optimal state encountered by the optimization operation.</returns>
        public override TState Optimize(TState initialState)
        {
            int stepCount = 0;
            TState currentState;
            TState nextState;

            currentState = initialState;

            while (true)
            {
                nextState = successorPicker.Next(currentState);

                if (currentState.GetEvaluation().Equals(nextState.GetEvaluation()))
                {
                    return currentState;
                }

                currentState = nextState;
                EmitState(currentState, ++stepCount);
            }
        }

        private void EmitState(TState currentState, int stepCount)
        {
            ClimbStepPerformedEvent?.Invoke(this, new ClimberStepEvent<TState, TEvaluation>()
            {
                StepsPerformed = stepCount,
                CurrentState = currentState,
            });
        }
    }
}
