using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazer.TBOptimizer.Climber.Algorithm
{
    public interface IClimberAlgorithm<TState, TEvaluation>
        where TState : IEvaluable<TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        TState Optimize(TState initialState);

        ISuccessorPicker<TState, TEvaluation> GetSuccessorPicker();

        IComparer<TEvaluation> GetComparisonStrategy();
    }
}
