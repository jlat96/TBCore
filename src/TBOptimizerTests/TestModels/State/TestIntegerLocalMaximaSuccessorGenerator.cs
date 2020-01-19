using System;
using System.Collections.Generic;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.TestModels.State
{
    public class TestIntegerLocalMaximaSuccessorGenerator : ISuccessorGenerator<TestIntegerEvaluableState, int>
    {
        public TestIntegerLocalMaximaSuccessorGenerator()
        {
        }

        /// <summary>
        /// Creates a successor function that will rise until x = 50, then decease until 100, then increase indefinately
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public IEnumerable<TestIntegerEvaluableState> GetSuccessors(TestIntegerEvaluableState state)
        {
            List<TestIntegerEvaluableState> neighbors = new List<TestIntegerEvaluableState>()
            {
                new TestIntegerEvaluableState(state.Value - 1)
            };

            if (state.Value < 50 || state.Value > 100)
            {
                neighbors.Add(new TestIntegerEvaluableState(state.Value + 1));
            }
            else
            {
                neighbors.Add(new TestIntegerEvaluableState(state.Value - 2));
            }

            return neighbors;
        }
    }
}
