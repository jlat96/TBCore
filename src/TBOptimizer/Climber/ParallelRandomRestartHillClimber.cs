using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber
{
    public class ParallelRandomRestartHillClimber<TState, TEvaluation> : HillClimber<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        private readonly int numRestarts;
        private readonly ISuccessorSelector<TState, TEvaluation> stateRandomizer;

        public ParallelRandomRestartHillClimber(IComparer<TEvaluation> comparisonStrategy,
            ISuccessorGenerator<TState, TEvaluation> successorGenerator,
            ISuccessorSelector<TState, TEvaluation> randomStateSelector,
            int numRestarts) :
            base(comparisonStrategy, successorGenerator)
        {
            stateRandomizer = randomStateSelector;
            this.numRestarts = numRestarts;
        }

        public ParallelRandomRestartHillClimber(
            ISuccessorSelector<TState, TEvaluation> stateRandomizer,
            ClimberAlgorithm<TState, TEvaluation> algorithm,
            int numRestarts) : base(algorithm)
        {
            this.numRestarts = numRestarts;
            this.stateRandomizer = stateRandomizer;
        }

        public override TState Optimize(TState initialState)
        {
            TState winnerState = null;
            Parallel.For<TState>(0, numRestarts, () => initialState, (i, s, localWinner) =>
            {
                TState nextRestart = stateRandomizer.Next(initialState);
                localWinner = base.Optimize(initialState);
                return localWinner;
            },
            (localWinner) =>
            {
                if (localWinner > winnerState) {
                    winnerState = localWinner;
                }
            });

            return winnerState;
        }
    }
}
