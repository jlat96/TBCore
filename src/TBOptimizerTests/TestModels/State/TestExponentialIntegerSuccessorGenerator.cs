using System;
using System.Collections.Generic;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizerTests.TestModels.State
{
    public class TestExponentialIntegerSuccessorGenerator : ISuccessorGenerator<TestIntegerEvaluableState, int>
    {
        private int initialValue;
        private int maxValue;
        private int runCount;

        public TestExponentialIntegerSuccessorGenerator()
        {
            Initialize();
        }

        public void Initialize()
        {
            maxValue = int.MaxValue;
            runCount = 0;
        }

        public IEnumerable<TestIntegerEvaluableState> GetSuccessors(TestIntegerEvaluableState current)
        {
            if (runCount++ == 0)
            {
                initialValue = current.Value;
                try
                {
                    maxValue = Convert.ToInt32(Math.Pow(current.Value, 4));
                }
                catch (Exception)
                {
                    maxValue = int.MaxValue;
                }
            }

            List<TestIntegerEvaluableState> neighbors = new List<TestIntegerEvaluableState>()
            {
                new TestIntegerEvaluableState(current.Value - 1),
            };

            if (current.Value < maxValue)
            {
                neighbors.Add(new TestIntegerEvaluableState(current.Value * current.Value));
            }

            return neighbors;
        }
    }
}
