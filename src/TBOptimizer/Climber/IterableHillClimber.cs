using System;
using System.Collections.Generic;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber
{
    /// <summary>
    /// IterableHillClimber is a classification of HillClimber that will iterate through several different HillClimber optimizations to produce a potentially more optimal result
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluation"></typeparam>
    public abstract class IterableHillClimber<TState, TEvaluation> : HillClimber<TState, TEvaluation>, IIterableClimber<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        /// <summary>
        /// Creates a IterableHillClimber that will optimize using the given comparer and successor generator
        /// </summary>
        /// <param name="comparer">The comparison strategy to optimize with</param>
        /// <param name="successorGenerator">The successor genereator from which the best state will be selected</param>
        public IterableHillClimber(IComparer<TEvaluation> comparer, ISuccessorGenerator<TState, TEvaluation> successorGenerator)
            : base(comparer, successorGenerator) { }


        /// <summary>
        /// Creates a IterableHillClimber that will perform an optimization from the given ClimberAlgrithm.
        /// </summary>
        /// <param name="algorithm">The climber algorithm to use for optimzation</param>
        public IterableHillClimber(ClimberAlgorithm<TState, TEvaluation> algorithm) : base(algorithm) { }

        public EventHandler<ClimberCompleteEvent<TState, TEvaluation>> ClimberCompleteEvent { get; set; }
    }
}
