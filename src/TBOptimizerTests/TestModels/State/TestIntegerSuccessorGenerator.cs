using System;
using System.Collections.Generic;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.TestModels.State
{
    public class TestIntegerSuccessorGenerator : ISuccessorGenerator<TestIntegerEvaluableState, int>
    {
        public IEnumerable<TestIntegerEvaluableState> GetSuccessors(TestIntegerEvaluableState state)
        {
            List<TestIntegerEvaluableState> neighbors = new List<TestIntegerEvaluableState>()
            {
                new TestIntegerEvaluableState(state.Value - 1),
            };

            if (state.Value < 100)
            {
                neighbors.Add(new TestIntegerEvaluableState(state.Value + 1));
            }

            return neighbors;
        }
    }
}
