using System;
using System.Collections.Generic;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState">The type of the state being optimized</typeparam>
    /// <typeparam name="TEvaluation">The type of the Evaluation of TState</typeparam>
    public abstract class ClimberAlgorithm<TState, TEvaluation> : IClimberAlgorithm<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected IComparer<TEvaluation> comparisonStrategy;
        protected ISuccessorSelector<TState, TEvaluation> successorPicker;

        /// <summary>
        /// Creates a new ClimberAlgorithm using the given comparison strategy to compare
        /// evaluations and the given successorPicker to select the next EvaluableState to
        /// evaluate against.
        /// </summary>
        /// <param name="comparisonStrategy">The Comparer that which Optimize will use to compare optimizations</param>
        /// <param name="successorPicker">The successor picker to choose the next EvaluableState in to evaluate at an optimization step</param>
        /// 
        protected ClimberAlgorithm(IComparer<TEvaluation> comparisonStrategy,
            ISuccessorSelector<TState, TEvaluation> successorPicker)
        {
            this.successorPicker = successorPicker;
            this.comparisonStrategy = comparisonStrategy;
        }

        public EventHandler<ClimberStepEvent<TState, TEvaluation>> ClimbStepPerformedEvent;

        /// <summary>
        /// The comparison strategy that Optimize will use to compare evaluations.
        /// </summary>
        public IComparer<TEvaluation> ComparisonStrategy {
            get => comparisonStrategy;

            internal set
            {
                comparisonStrategy = value ?? throw new ArgumentException("ComparisonStrategy cannot be null");
            }
        }

        /// <summary>
        /// The successor picker that will choose the next EvaluableState at an optimization stage.
        /// </summary>
        public ISuccessorSelector<TState, TEvaluation> SuccessorPicker {
            get => successorPicker;
            internal set
            {
                successorPicker = value ?? throw new ArgumentException("SuccessorPicker cannot be null");
            }
        }

        /// <summary>
        /// Performs the HillClimber optimization as specified by the child class.
        /// </summary>
        /// <param name="initialState"></param>
        /// <returns></returns>
        public abstract TState Optimize(TState initialState);
    }
}
