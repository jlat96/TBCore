using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer.Climber.Algorithm
{
    public class NumericClimberAlgorithm<TState> : LocalClimberAlgorithm<TState, int>
        where TState : EvaluableState<TState, int>
    {
        public NumericClimberAlgorithm(IComparer<int> comparer, ISuccessorPicker<TState, int> successorPicker) : base(comparer, successorPicker) { }
        public NumericClimberAlgorithm(bool greedy, IComparer<int> comperer, ISuccessorPicker<TState, int> successorPicker) : base(greedy, comperer, successorPicker) { }
    }
}
