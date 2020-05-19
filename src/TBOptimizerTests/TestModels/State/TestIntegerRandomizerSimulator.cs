using System;
using System.Collections.Concurrent;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.TestModels.State
{
    public class TestIntegerRandomizerSimulator : ISuccessorSelector<TestIntegerEvaluableState, int>
    {
        private readonly ConcurrentQueue<int> numberSequence;

        public TestIntegerRandomizerSimulator()
        {
            numberSequence = new ConcurrentQueue<int>();
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 2; i <= 10; i += 2)
            {
                numberSequence.Enqueue(i);
            }
        }

        public TestIntegerEvaluableState Next(TestIntegerEvaluableState current)
        {
            int next;
            while (!numberSequence.TryDequeue(out next))
            {
                if (numberSequence.Count < 1)
                {
                    Initialize();
                }
            }

            return new TestIntegerEvaluableState(next);
        }
    }
}
