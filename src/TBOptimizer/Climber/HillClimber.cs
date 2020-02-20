using System;
using System.Collections.Generic;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber
{
    /// <summary>
    /// HillClimber is an optimizer that will perform a local search hill climbing optimization on an initial state, continuing the
    /// operation until no more optimal state can be generated from a generated neighborhood.
    /// </summary>
    /// <typeparam name="TState">The type of the EvaluableState that is being evaluated</typeparam>
    /// <typeparam name="TEvaluation">The type of the Comparable result of an evaluation</typeparam>
    public class HillClimber<TState, TEvaluation> : Optimizer<TState, TEvaluation>, IClimberEventHandler<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected readonly ClimberAlgorithm<TState, TEvaluation> algorithm;

        /// <summary>
        /// Creates a HillClimber that will optimize using the given comparer and successor generator
        /// </summary>
        /// <param name="comparer">The comparison strategy to optimize with</param>
        /// <param name="successorGenerator">The successor genereator from which the best state will be selected</param>
        public HillClimber(IComparer<TEvaluation> comparer, ISuccessorGenerator<TState, TEvaluation> successorGenerator)
            : this(new LocalClimberAlgorithm<TState, TEvaluation>(comparer, new ClimberSuccessorPicker<TState, TEvaluation>(successorGenerator, comparer))) { }

        /// <summary>
        /// Creates a HillClimber that will perform an optimization from the given ClimberAlgrithm.
        /// </summary>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        public HillClimber(ClimberAlgorithm<TState, TEvaluation> algorithm) : base (algorithm.SuccessorPicker)
        {
            this.algorithm = algorithm;
            this.algorithm.ClimbStepPerformed += OnClimberStepEvent;
        }

        public EventHandler<ClimberStepEvent<TState, TEvaluation>> ClimberStepPerformedEvent;

        public TState Optimize(TState initialState)
        {
            TState finalState = PerformOptimization(initialState);
            return finalState;
        }

        /// <summary>
        /// Performs the climber optimization from a given initialState
        /// </summary>
        /// <param name="initialState">The initial state for which to optimize</param>
        /// <returns>The most optimal state encountered by the climber. This may not be the best possible state</returns>
        public override TState PerformOptimization(TState initialState)
        {
            return algorithm.Optimize(initialState);
        }

        public void OnClimberStepEvent(object sender, ClimberStepEvent<TState, TEvaluation> e)
        {
            ClimberStepPerformedEvent?.Invoke(sender, e);
        }
    }
}
