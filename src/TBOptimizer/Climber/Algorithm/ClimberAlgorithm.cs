using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    public abstract class ClimberAlgorithm<TState, TEvaluation> : IClimberAlgorithm<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected readonly bool greedy;
        protected readonly IComparer<TEvaluation> comparisonStrategy;
        protected readonly ISuccessorPicker<TState, TEvaluation> successorPicker;

        public ClimberAlgorithm(IComparer<TEvaluation> comparisonStrategy, ISuccessorPicker<TState, TEvaluation> successorPicker) : this(false, comparisonStrategy, successorPicker) { }

        public ClimberAlgorithm(bool greedy, IComparer<TEvaluation> comparisonStrategy, ISuccessorPicker<TState, TEvaluation> successorPicker)
        {
            this.successorPicker = successorPicker;
            this.comparisonStrategy = comparisonStrategy;
            this.greedy = greedy;
        }

        public IComparer<TEvaluation> GetComparisonStrategy()
        {
            return comparisonStrategy;
        }

        public ISuccessorPicker<TState, TEvaluation> GetSuccessorPicker()
        {
            return successorPicker;
        }

        public abstract TState Optimize(TState initialState);
    }
}
