using System;
using System.Collections.Generic;

namespace TrailBlazer.TBOptimizer.State
{
    public class FunctionalSuccessorGenerator<TState, TEvaluation> : ISuccessorGenerator<TState, TEvaluation>
        where TState : EvaluableState<TState, TEvaluation>
        where TEvaluation : IComparable<TEvaluation>
    {
        private Func<TState, IEnumerable<TState>> generationFunction;

        public FunctionalSuccessorGenerator(Func<TState, IEnumerable<TState>> generationFunction)
        {
            this.generationFunction = generationFunction;
        }

        public IEnumerable<TState> GetSuccessors(TState state)
        {
            return generationFunction.Invoke(state);
        }
    }
}
