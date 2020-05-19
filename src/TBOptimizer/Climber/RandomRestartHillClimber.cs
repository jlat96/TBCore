using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBOptimizer.Climber;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Configuration;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    /// <summary>
    /// RandomRestartHillClimber is a HillClimber optimizer that runs a Climber optimization operation many times.
    /// </summary>
    /// <typeparam name="TState">The type of the EvaluableState that is being evaluated</typeparam>
    /// <typeparam name="TEvaluation">The type of the Comparable result of an evaluation</typeparam>
    public class RandomRestartHillClimber<TState, TEvaluation> : IterableHillClimber<TState, TEvaluation>, IClimberEventHandler<TState, TEvaluation>
        where TState : EvaluableState<TState,TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        private readonly uint numRestarts;
        private readonly Func<TState, TState> randomizationFunction;
        private readonly IClimberConfiguration<TState, TEvaluation> iterationClimberConfiguration;

        /// <summary>
        /// Creates a new <see cref="RandomRestartHillClimber{TState, TEvaluation}"/> that will perform numRestarts number of climber operations
        /// </summary>
        /// <param name="numRestarts">The number of climber operations to perform before returning a result</param>
        /// <param name="stateRandomizer">A complex state randomization strategy</param>
        /// <param name="climberConfiguration">A configuration for the climber that will be executed at each restart</param>
        public RandomRestartHillClimber(uint numRestarts, ISuccessorSelector<TState, TEvaluation> stateRandomizer, IClimberConfiguration<TState, TEvaluation> climberConfiguration)
            :this(numRestarts, climberConfiguration, c => stateRandomizer.Next(c))
        {
        }

        /// <summary>
        /// Creates a new <see cref="RandomRestartHillClimber{TState, TEvaluation}"/> that will perform numRestarts number of climber operations
        /// </summary>
        /// <param name="numRestarts">The number of climber operations to perform before returning a result</param>
        /// <param name="climberConfiguration">A configuration for the climber that will be executed at each restart</param>
        /// <param name="randomizationFunction">A function that will be given an instance of <see cref="TState"/> and should return a random <see cref="TState"/></param>
        public RandomRestartHillClimber(uint numRestarts, IClimberConfiguration<TState, TEvaluation> climberCofiguration, Func<TState, TState> randomizationFunction)
        {
            this.numRestarts = numRestarts;
            this.randomizationFunction = randomizationFunction;
            iterationClimberConfiguration = climberCofiguration;
        }

        /// <summary>
        /// Performs a numRestarts number of hill climbing operation starting at a randomized state. Returns the most
        /// optimal result of all performed operations. The internal climber created by the given climber configuration
        /// will be reinitialized between climber steps to ensure that encountered states from any optimization will
        /// have no impact on subsequent optimizations on a random state
        /// </summary>
        /// <param name="initialState">The initial state for which to optimize</param>
        /// <returns>The most optimal encountered <see cref="TState"/> from the restarted operation</returns>
        public override TState Optimize(TState initialState)
        {
            TState winnerState = initialState;
            IHillClimber<TState, TEvaluation> innerClimber;
            for (int i = 0; i < numRestarts; i++)
            {
                TState nextRestart = randomizationFunction.Invoke(initialState);
                innerClimber = iterationClimberConfiguration.Build();
                innerClimber.ClimberStepPerformedEvent += OnClimberStepEvent;
                TState localWinner = innerClimber.Optimize(nextRestart);

                if (localWinner > winnerState)
                {
                    winnerState = localWinner;
                }

                EmitRestartCompletion(i, 0, nextRestart, localWinner);

            }
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

        public void OnClimberStepEvent(object sender, ClimberStepEvent<TState, TEvaluation> e)
        {
            ClimberStepPerformedEvent?.Invoke(sender, e);
        }
    }
}
