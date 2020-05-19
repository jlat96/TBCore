using System;
using System.Collections.Generic;
using TBOptimizer.Climber;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    /// <summary>
    /// HillClimber is an optimizer that will perform a local search hill climbing optimization on an initial state, continuing the
    /// operation until no more optimal state can be generated from a generated neighborhood. HillClimber is NOT thread safe
    /// </summary>
    /// <typeparam name="TState">The type of the EvaluableState that is being evaluated</typeparam>
    /// <typeparam name="TEvaluation">The type of the Comparable result of an evaluation</typeparam>
    /// <threadsafety/>
    public class HillClimber<TState, TEvaluation> : IHillClimber<TState, TEvaluation>, IClimberEventHandler<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected readonly IClimberAlgorithm<TState, TEvaluation> algorithm;
        protected readonly ISuccessorSelector<TState, TEvaluation> successorSelector;

        /// <summary>
        /// Creates a HillClimber that will perform an optimization from the given ClimberAlgrithm.
        /// </summary>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        public HillClimber(IClimberAlgorithm<TState, TEvaluation> algorithm)
        {
            this.algorithm = algorithm;
            this.algorithm.ClimbStepPerformedEvent += OnClimberStepEvent;
        }

        /// <inheritdoc/>
        public EventHandler<ClimberStepEvent<TState, TEvaluation>> ClimberStepPerformedEvent { get; set; }

        /// <summary>
        /// Performs the climber optimization from a given initialState
        /// </summary>
        /// <param name="initialState">The initial state for which to optimize</param>
        /// <returns>The most optimal state encountered by the climber. This may not be the best possible state</returns>
        public virtual TState Optimize(TState initialState)
        {
            return algorithm.Optimize(initialState);
        }

        public void OnClimberStepEvent(object sender, ClimberStepEvent<TState, TEvaluation> e)
        {
            ClimberStepPerformedEvent?.Invoke(sender, e);
        }
    }
}
