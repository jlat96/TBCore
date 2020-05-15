using System;
using System.Collections.Generic;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizerTests.Randomizer
{
    public class TestStateRandomizer : ISuccessorSelector<TestIntegerEvaluableState, int>
    {
        private readonly int minNumber;
        private readonly int maxNumber;
        private readonly int maxIterations;
        private int current;
        public TestStateRandomizer(int minNumber, int maxOffset, int maxIterations)
        {
            current = 0;
            this.minNumber = minNumber;
            this.maxNumber = minNumber + maxOffset;
            this.maxIterations = maxIterations;
            randomNumbers = GetRandomNumbers();
        }

        public List<int> randomNumbers { get; private set; }

        private List<int> GetRandomNumbers()
        {
            List<int> numbers = new List<int>();
            int prev = minNumber;
            Random randy = new Random();
            for (int i = minNumber; i < maxIterations; i++)
            {
                int tailOffset = maxNumber - (1 + i);
                int next = prev < maxNumber ? randy.Next(prev, maxNumber - tailOffset) : maxNumber;
                prev = next;
                numbers.Add(next);
            }

            return numbers;
        }

        public TestIntegerEvaluableState Next(TestIntegerEvaluableState incoming)
        {
            TestIntegerEvaluableState state = new TestIntegerEvaluableState(randomNumbers[current++ % maxIterations]);
            return state;
        }
    }
}
