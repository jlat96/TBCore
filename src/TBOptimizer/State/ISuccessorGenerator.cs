using System;
using System.Collections.Generic;
using System.Text;

namespace TrailBlazer.TBOptimizer.State
{
    public interface ISuccessorGenerator<TState, TEvaluable> where TState : IEvaluable<TEvaluable>
        where TEvaluable : IComparable<TEvaluable>
    {
        IEnumerable<TState> GetSuccessors(TState state);
    }
}
