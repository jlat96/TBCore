using System;
using System.Collections.Generic;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using Trailblazer.TBOptimizer.Randomization;
using TrailBlazer.TBOptimizer.Comparison;

namespace TBOptimizerTests.Randomizer
{
    [TestFixture]
    public class TestRandomizer
    {
        IComparer<int> comparer;

        [SetUp]
        public void Init()
        {
            comparer = new MaximizingComparer<int>();
        }

        [Test, Timeout(5000)]
        public void TestOptimizeSingleIteration()
        {
            TestStateRandomizer stateSelector = new TestStateRandomizer(3, 97, 80);
            MonteCarloRandomizer<TestIntegerEvaluableState, int> randomizer = new MonteCarloRandomizer<TestIntegerEvaluableState, int>(comparer, stateSelector);

            int expectedEvaluation = stateSelector.randomNumbers[0];

            TestIntegerEvaluableState initial = new TestIntegerEvaluableState(2);

            TestIntegerEvaluableState result = randomizer.Optimize(initial);

            Assert.AreEqual(expectedEvaluation, result.GetEvaluation());
        }

        [Test, Timeout(5000)]
        public void TestOptimizerMultiIterationStopsAtGivenNumber()
        {
            for (int i = 1; i < 70; i++)
            {
                DoNumRandomizations(i, 3, 97, 80);
            }
        }

        private void DoNumRandomizations(int stoppingPoint, int minNumber, int offset, int numNumbers)
        {
            TestStateRandomizer stateSelector = new TestStateRandomizer(minNumber, offset, numNumbers);
            MonteCarloRandomizer<TestIntegerEvaluableState, int> randomizer = new MonteCarloRandomizer<TestIntegerEvaluableState, int>(comparer, stateSelector, stoppingPoint);

            int expectedEvaluation = stateSelector.randomNumbers[stoppingPoint - 1];

            TestIntegerEvaluableState initial = new TestIntegerEvaluableState(2);
            TestIntegerEvaluableState result = randomizer.Optimize(initial);

            Assert.AreEqual(expectedEvaluation, result.GetEvaluation());
        }

        [Test, Timeout(5000)]
        public void TestOptimizerUnlimitedIterationsStopsWhenBetterStateIsReached()
        {
            for (int i = 200; i > 0; i -= 2)
            {
                DoLowPointRandomizations(i, i / 2);
            }
        }

        private void DoLowPointRandomizations(int initialValue, int lowPoint)
        {
            TestStateDipRandomizer stateSelector = new TestStateDipRandomizer(lowPoint);
            MonteCarloRandomizer<TestIntegerEvaluableState, int> randomizer = new MonteCarloRandomizer<TestIntegerEvaluableState, int>(comparer, stateSelector, -1);

            TestIntegerEvaluableState initial = new TestIntegerEvaluableState(initialValue);

            TestIntegerEvaluableState result = randomizer.Optimize(initial);

            Assert.AreEqual(initialValue + 1, result.GetEvaluation());
        }

        [Test, Timeout(5000)]
        public void TestRandomizerUnlimitedIterationsStopWhenTimeoutIsPassed()
        {

        }
    }
}
