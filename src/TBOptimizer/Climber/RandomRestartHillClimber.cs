using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    /// <summary>
    /// RandomRestartHillClimber is a HillClimber optimizer that runs a Climber operation many times. If the
    /// climber is setup to be parallel options are given, the individual climber operations will attempt to
    /// be completed in parallel.
    /// </summary>
    /// <typeparam name="TState">The type of the EvaluableState that is being evaluated</typeparam>
    /// <typeparam name="TEvaluation">The type of the Comparable result of an evaluation</typeparam>
    public class RandomRestartHillClimber<TState, TEvaluation> : HillClimber<TState, TEvaluation>
        where TState : EvaluableState<TState,TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        private readonly uint numRestarts;
        private readonly ISuccessorSelector<TState, TEvaluation> stateRandomizer;

        /// <summary>
        /// Creates a new RandomRestartHillClimber that will perform numRestarts local serach optimizations using the given comparer and successor generator 
        /// </summary>
        /// <param name="comparisonStrategy">The comparison strategy to optimize with</param>
        /// <param name="successorGenerator">The successor generator from which the most optimal will be selected</param>
        /// <param name="numRestarts">The number of optimization restarts to perform</param>
        public RandomRestartHillClimber(
            IComparer<TEvaluation> comparisonStrategy,
            ISuccessorGenerator<TState, TEvaluation> successorGenerator,
            ISuccessorSelector<TState, TEvaluation> randomStateSelector,
            uint numRestarts) :
            base (comparisonStrategy, successorGenerator)
        {
            stateRandomizer = randomStateSelector;
            this.numRestarts = numRestarts;
        }

        /// Creates a RandomRestartHillClimber
        /// <param name="stateRandomizer">A successor picker that will generate a random successor state from the initial state</param>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        /// <param name="numRestarts">The number of random restart operations to complete</param>
        public RandomRestartHillClimber(ISuccessorSelector<TState, TEvaluation> stateRandomizer, ClimberAlgorithm<TState, TEvaluation> algorithm, uint numRestarts) : base (algorithm)
        {
            this.numRestarts = numRestarts;
            this.stateRandomizer = stateRandomizer;
        }

        public EventHandler<ClimberRestartResultEvent<TState, TEvaluation>> RestartCompletedEvent;

        /// <summary>
        /// Performs a monte carlo random restart hill climbing operation in parallel up to numRestarts. If parallel
        /// properties were specified at initialization, the climbing operation will attempt to execute in parallel
        /// </summary>
        /// <param name="initialState">The initial state for which to optimize</param>
        /// <returns>The most optimal encountered state from the restarted operation</returns>
        public override TState Optimize(TState initialState)
        {
            List<TState> winnerStates = new List<TState>();
            for (int i = 0; i < numRestarts; i++)
            { 
                TState nextRestart = stateRandomizer.Next(initialState);
                TState localWinner = base.Optimize(nextRestart);
                lock (winnerStates)
                {
                    winnerStates.Add(localWinner);
                }
                EmitRestartCompletion(i, 0, nextRestart, localWinner);
            }
            return winnerStates.OrderByDescending(s => s.GetEvaluation()).First();
        }

        private void EmitRestartCompletion(int restartNumber, int totalSteps, TState initialState, TState optimizedState)
        {
            RestartCompletedEvent?.Invoke(this, new ClimberRestartResultEvent<TState, TEvaluation>()
            {
                RestartNumber = restartNumber,
                TotalStepsPerformed = totalSteps,
                InitialState = initialState,
                OptimizedState = optimizedState,
            });
        }
    }
}
