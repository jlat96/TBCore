using System;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <typeparam name="TEvaluation"></typeparam>
    public abstract class Optimizer<TState, TEvaluation> : IOptimizer<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        public abstract TState Optimize(TState initialState);
    }
}
