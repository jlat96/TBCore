using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ParallelOptions parallelOptions;
        private readonly ISuccessorPicker<TState, TEvaluation> stateRandomizer;

        /// <summary>
        /// Creates a RandomRestartHillClimber that will restart the Optimization from the given ClimberAlgorithm
        /// up to numRestarts in serial (one thread)
        /// </summary>
        /// <param name="stateRandomizer">A successor picker that will generate a random successor state from the initial state</param>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        /// <param name="numRestarts">The number of random restart operations to complete</param>
        public RandomRestartHillClimber(ISuccessorPicker<TState, TEvaluation> stateRandomizer, ClimberAlgorithm<TState, TEvaluation> algorithm, uint numRestarts)
            : this(stateRandomizer, algorithm, numRestarts, false) { }

        /// <summary>
        /// Creates a RandomRestartHillClimber that will restart the Optimization from the given ClimberAlgorithm
        /// up to numRestarts. If parallel is true, the operation will be completed with all available processors
        /// </summary>
        /// <param name="stateRandomizer">A successor picker that will generate a random successor state from the initial state</param>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        /// <param name="numRestarts">The number of random restart operations to complete</param>
        /// <param name="parallel">Flag to determine if the process should run in parallel</param>
        public RandomRestartHillClimber(ISuccessorPicker<TState, TEvaluation> stateRandomizer, ClimberAlgorithm<TState, TEvaluation> algorithm, uint numRestarts, bool parallel)
            : this(stateRandomizer, algorithm, numRestarts, parallel ? -1 : 1) { }

        /// <summary>
        /// Creates a RandomRestartHillClimber that will restart the Optimization from the given ClimberAlgorithm
        /// up to numRestarts. The operation will run in parallel with as many available processors up to maxDegreeOfParallelism.
        /// </summary>
        /// <param name="stateRandomizer">A successor picker that will generate a random successor state from the initial state</param>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        /// <param name="numRestarts">The number of random restart operations to complete</param>
        /// <param name="maxDegreeOfParallelism">The maximum number of processors that will attempt to perform an optimization operation</param>
        public RandomRestartHillClimber(ISuccessorPicker<TState, TEvaluation> stateRandomizer, ClimberAlgorithm<TState, TEvaluation> algorithm, uint numRestarts, int maxDegreeOfParallelism)
            : this(stateRandomizer, algorithm, numRestarts, new ParallelOptions() { MaxDegreeOfParallelism = maxDegreeOfParallelism }) { }

        /// <summary>
        /// Creates a RandomRestartHillClimber that will restart the Optimization from the given ClimberAlgorithm
        /// up to numRestarts in parallel with the given ParallelOptions
        /// </summary>
        /// <param name="stateRandomizer">A successor picker that will generate a random successor state from the initial state</param>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        /// <param name="numRestarts">The number of random restart operations to complete</param>
        /// <param name="parallelOptions">The options for the Parallel restart operations</param>
        public RandomRestartHillClimber(ISuccessorPicker<TState, TEvaluation> stateRandomizer, ClimberAlgorithm<TState, TEvaluation> algorithm, uint numRestarts, ParallelOptions parallelOptions) : base (algorithm)
        {
            this.numRestarts = numRestarts;
            this.parallelOptions = parallelOptions;
            this.stateRandomizer = stateRandomizer;
        }

        /// <summary>
        /// Performs a monte carlo random restart hill climbing operation in parallel up to numRestarts. If parallel
        /// properties were specified at initialization, the climbing operation will attempt to execute in parallel
        /// </summary>
        /// <param name="initialState">The initial state for which to optimize</param>
        /// <returns>The most optimal encountered state from the restarted operation</returns>
        public override TState PerformOptimization(TState initialState)
        {
            List<TState> winnerStates = new List<TState>();
            for (int i = 0; i < numRestarts; i++)
            { 
                TState nextRestart = stateRandomizer.Next(initialState);
                TState localWinner = base.PerformOptimization(nextRestart);
                lock (winnerStates)
                {
                    winnerStates.Add(localWinner);
                }
            }
            return winnerStates.OrderByDescending(s => s.GetEvaluation()).First();
        }
    }
}
