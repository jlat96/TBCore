using System;
using System.Collections.Generic;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    /// <summary>
    /// Represents the basis for a Hill Climbing optimization algorithm
    /// </summary>
    /// <typeparam name="TState">The type of the state being optimized</typeparam>
    /// <typeparam name="TEvaluation">The type of the Evaluation of TState</typeparam>
    public abstract class ClimberAlgorithm<TState, TEvaluation> : IClimberAlgorithm<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected ISuccessorSelector<TState, TEvaluation> successorPicker;

        /// <summary>
        /// Creates a new ClimberAlgorithm using the given comparison strategy to compare
        /// evaluations and the given successorPicker to select the next EvaluableState to
        /// evaluate against.
        /// </summary>
        /// <param name="successorPicker">The successor picker to choose the next EvaluableState in to evaluate at an optimization step</param>
        protected ClimberAlgorithm(ISuccessorSelector<TState, TEvaluation> successorPicker)
        {
            this.successorPicker = successorPicker;
        }

        public EventHandler<ClimberStepEvent<TState, TEvaluation>> ClimbStepPerformedEvent { get; set; }

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
