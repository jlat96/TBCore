using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.State
{
    public abstract class StatePicker<TState, TEvaluable> : ISuccessorPicker<TState, TEvaluable>
        where TState : EvaluableState<TState, TEvaluable>
        where TEvaluable : IComparable<TEvaluable>
    {
        protected ISet<TState> encounteredStates;

        public StatePicker()
        {
            encounteredStates = new HashSet<TState>();
        }

        public TState Next(TState current)
        {
            TState next = SelectNext(current);
            encounteredStates.Add(next);
            return next;
        }

        protected abstract TState SelectNext(TState current);
    }
}
