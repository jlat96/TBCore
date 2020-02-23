using System;
using System.Collections.Generic;
using OptimizerTests.TestModels.Evaluable;
using TBOptimizer.State;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.TestModels.State
{
    public class TestIntegerRandomizerSimulator : ISuccessorSelector<TestIntegerEvaluableState, int>
    {
        private readonly List<int> numberSequence;
        private int sequenceNumber;

        public TestIntegerRandomizerSimulator()
        {
            numberSequence = new List<int>() { 2, 4, 6, 8, 10 };
            sequenceNumber = 0;
        }

        public TestIntegerEvaluableState Next(TestIntegerEvaluableState current)
        {
            int next;
            lock (numberSequence)
            {
                 next = numberSequence[sequenceNumber++ % (numberSequence.Count)];
            }
            return new TestIntegerEvaluableState(next);
        }
    }
}
