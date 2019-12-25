using System;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer
{
    public abstract class Optimizer<TState, TEvaluation> : IOptimizer<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        protected ISuccessorPicker<TState, TEvaluation> successorPicker;

        protected Optimizer(ISuccessorPicker<TState, TEvaluation> successorPicker)
        {
            this.successorPicker = successorPicker;
        }

        public abstract TState PerformOptimization(TState initialState);
    }
}
