using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber
{
    /// <summary>
    /// ParallelRandomRestartHillClimber is a HillClimber optimizer that runs a Climber Optimize operation many
    /// times in parallel.
    /// </summary>
    /// <typeparam name="TState">The type of the evaulable being evaluated</typeparam>
    /// <typeparam name="TEvaluation">The type of the evaulation</typeparam>
    public class ParallelRandomRestartHillClimber<TState, TEvaluation> : IterableHillClimber<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        private readonly ParallelOptions parallelOptions;
        private readonly int numRestarts;
        private readonly ISuccessorSelector<TState, TEvaluation> stateRandomizer;

        public ParallelRandomRestartHillClimber(
            IComparer<TEvaluation> comparisonStrategy,
            ISuccessorGenerator<TState, TEvaluation> successorGenerator,
            ISuccessorSelector<TState, TEvaluation> randomStateSelector,
            int numRestarts) : this (
                comparisonStrategy,
                successorGenerator,
                randomStateSelector,
                numRestarts,
                new ParallelOptions())
        {
            stateRandomizer = randomStateSelector;
            this.numRestarts = numRestarts;
        }

        public ParallelRandomRestartHillClimber(
            IComparer<TEvaluation> comparisonStrategy,
            ISuccessorGenerator<TState, TEvaluation> successorGenerator,
            ISuccessorSelector<TState, TEvaluation> randomStateSelector,
            int numRestarts,
            ParallelOptions parallelOptions) : base(
                comparisonStrategy,
                successorGenerator)
        {
            this.parallelOptions = parallelOptions;
            stateRandomizer = randomStateSelector;
            this.numRestarts = numRestarts;
        }

        public ParallelRandomRestartHillClimber(
            ISuccessorSelector<TState, TEvaluation> stateRandomizer,
            ClimberAlgorithm<TState, TEvaluation> algorithm,
            int numRestarts) : this (
                stateRandomizer,
                algorithm,
                numRestarts,
                new ParallelOptions())
        {
            this.numRestarts = numRestarts;
            this.stateRandomizer = stateRandomizer;
        }

        public ParallelRandomRestartHillClimber(
            ISuccessorSelector<TState, TEvaluation> stateRandomizer,
            ClimberAlgorithm<TState, TEvaluation> algorithm,
            int numRestarts,
            ParallelOptions parallelOptions) : base (algorithm)
        {
            this.parallelOptions = parallelOptions;
            this.numRestarts = numRestarts;
            this.stateRandomizer = stateRandomizer;
        }

        public override TState Optimize(TState initialState)
        {
            TState winnerState = initialState;
            _ = Parallel.For(0, numRestarts, () => initialState, (i, s, localWinner) =>
              {
                  TState nextRestart = stateRandomizer.Next(initialState);
                  localWinner = base.Optimize(initialState);
                EmitRestartCompletion(i, 0, nextRestart, localWinner);
                return localWinner;
              },
            (localWinner) =>
            {
                if (localWinner > winnerState)
                {
                    winnerState = localWinner;
                }
            });

            return winnerState;
        }

        private void EmitRestartCompletion(int restartNumber, int totalSteps, TState initialState, TState optimizedState)
        {
            ClimberCompleteEvent?.Invoke(this, new ClimberCompleteEvent<TState, TEvaluation>()
            {
                CLimberIndex = restartNumber,
                TotalStepsPerformed = totalSteps,
                InitialState = initialState,
                OptimizedState = optimizedState,
            });
        }
    }
}
