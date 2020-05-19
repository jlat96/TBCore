using System;
using System.Collections.Generic;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.TestModels.State
{
    public class TestLinearIntegerSuccessorGenerator : ISuccessorGenerator<TestIntegerEvaluableState, int>
    {

        private readonly int incrementStep;
        private readonly int maxValue;

        public TestLinearIntegerSuccessorGenerator() : this(1) { }

        public TestLinearIntegerSuccessorGenerator(int incrementStep)
        {
            this.incrementStep = incrementStep;
            maxValue = incrementStep * 100;
        }

        /// <summary>
        /// Returns a better integer evaluable state until the next state reaches x = 100 * incrementStep
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public IEnumerable<TestIntegerEvaluableState> GetSuccessors(TestIntegerEvaluableState state)
        {
            List<TestIntegerEvaluableState> neighbors = new List<TestIntegerEvaluableState>()
            {
                new TestIntegerEvaluableState(state.Value - 1),
            };

            if (state.Value < maxValue)
            {
                neighbors.Add(new TestIntegerEvaluableState(state.Value + incrementStep));
            }

            return neighbors;
        }
    }
}
