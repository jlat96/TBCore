using System;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizerTests.Randomizer
{
    public class TestStateDipRandomizer : ISuccessorSelector<TestIntegerEvaluableState, int>
    {
        private int initial;
        private readonly int lowPoint;
        int count;

        public TestStateDipRandomizer(int lowPoint)
        {
            this.lowPoint = lowPoint;
            count = 0;
        }

        public TestIntegerEvaluableState Next(TestIntegerEvaluableState current)
        {
            if(count++ == 0)
            {
                initial = current.Value;
            }

            if (current.Value - count <= lowPoint)
            {
                return new TestIntegerEvaluableState(initial + 1);
            }

            return new TestIntegerEvaluableState(current.Value - count);
        }
    }
}
