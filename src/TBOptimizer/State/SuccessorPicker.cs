using System;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.State
{
    public abstract class StatePicker<TState, TEvaluable> : ISuccessorPicker<TState, TEvaluable>
        where TState : EvaluableState<TState, TEvaluable>
        where TEvaluable : IComparable<TEvaluable>
    {
        

        public TState Next(TState current)
        {
            throw new NotImplementedException();
        }

        protected abstract TState SelectNext(TState current);
    }
}
