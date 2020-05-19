using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.Climber.Algorithm
{
    [TestFixture]
    public class TestLocalClimberAlgorithm
    {
        ISuccessorGenerator<TestIntegerEvaluableState, int> generator;
        ClimberSuccessorSelector<TestIntegerEvaluableState, int> picker;
        ClimberAlgorithm<TestIntegerEvaluableState, int> algorithm;

        [SetUp]
        public void Setup()
        {
            generator = new TestLinearIntegerSuccessorGenerator();
            picker = new ClimberSuccessorSelector<TestIntegerEvaluableState, int>(generator, new MaximizingComparer<int>());
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(picker);
        }

        [Test, Timeout(5000)]
        public void TestOptimizeCorrectOptimalValueReached()
        {
            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(2);
            TestIntegerEvaluableState result = algorithm.Optimize(initialState);

            Assert.AreEqual(100, result.Value);

        }

        [Test]
        public void TestOptimizeStopsAtLocalExtrema()
        {
            IComparer<int> comparer = new MaximizingComparer<int>();
            generator = new TestIntegerLocalMaximaSuccessorGenerator();
            picker = new ClimberSuccessorSelector<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(picker);

            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(2);

            TestIntegerEvaluableState result = algorithm.Optimize(initialState);
            Assert.AreEqual(50, result.Value, "Optimized state is incorrect");
        }

        [Test]
        public void TestOptimizeEmitsEventsInOrder()
        {
            IComparer<int> comparer = new MaximizingComparer<int>();
            generator = new TestIntegerLocalMaximaSuccessorGenerator();
            picker = new ClimberSuccessorSelector<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(picker);

            List<int> encounteredStates = new List<int>();
            List<int> expectedStates = new List<int>();

            for (int i = 3; i <= 50; i++)
            {
                expectedStates.Add(i);
            }

            void OnEvent(object source, ClimberStepEvent<TestIntegerEvaluableState, int> e)
            {
                encounteredStates.Add(e.CurrentState.Value);
            }

            algorithm.ClimbStepPerformedEvent += OnEvent;

            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(2);
            TestIntegerEvaluableState result = algorithm.Optimize(initialState);

            Assert.AreEqual(expectedStates.Count, encounteredStates.Count);
            for (int i = 0; i < expectedStates.Count; i++)
            {
                Assert.AreEqual(encounteredStates[i], expectedStates[i], "Encountered states do not match");
            }
        }
    }
}
