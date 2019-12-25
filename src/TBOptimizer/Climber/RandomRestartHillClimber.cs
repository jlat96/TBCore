using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    public class RandomRestartHillClimber<TState, TEvaluation> : HillClimber<TState, TEvaluation>
        where TState : EvaluableState<TState,TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        private readonly int numRestarts;

        public RandomRestartHillClimber(ClimberAlgorithm<TState, TEvaluation> algorithm, int numRestarts) : base(algorithm)
        {
            this.numRestarts = numRestarts;
        }

        /// <summary>
        /// Performs a monte carlo random restart hill climbing operation in parallel up to numRestarts
        /// </summary>
        /// <param name="initialState">The initial state for which to optimize</param>
        /// <returns>The most optimal encountered state from the restarted operation</returns>
        public override TState PerformOptimization(TState initialState)
        {
            List<TState> winnerStates = new List<TState>();
            Parallel.For(0, numRestarts - 1, i => {
                TState localWinner = base.PerformOptimization(initialState);
                lock (winnerStates)
                {
                    winnerStates.Add(localWinner);
                }
            });
            return winnerStates.OrderByDescending(s => s.GetEvaluation()).First();
        }
    }
}
