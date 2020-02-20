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
        ClimberSuccessorPicker<TestIntegerEvaluableState, int> picker;
        ClimberAlgorithm<TestIntegerEvaluableState, int> algorithm;

        [SetUp]
        public void Setup()
        {
            generator = new TestLinearIntegerSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, new MaximizingComparer<int>());
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(new MaximizingComparer<int>(), picker);
        }

        [Test]
        public void TestOptimizeCorrectOptimalValueReached()
        {
            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(2);
            Task<TestIntegerEvaluableState> task = Task.Run(() => algorithm.Optimize(initialState));

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (!task.IsCompleted && timer.ElapsedMilliseconds < 5000)
            {
            }
            timer.Stop();

            Assert.IsTrue(task.IsCompleted, "Optimization took too long to complete");
            TestIntegerEvaluableState result = task.Result;

            Assert.AreEqual(100, result.Value);

        }

        [Test]
        public void TestOptimizeStopsAtLocalExtrema()
        {
            IComparer<int> comparer = new MaximizingComparer<int>();
            generator = new TestIntegerLocalMaximaSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);

            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(2);
            Task<TestIntegerEvaluableState> optimizeTask = Task.Run(() => algorithm.Optimize(initialState));

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (!optimizeTask.IsCompleted && timer.ElapsedMilliseconds < 10000)
            {
            }

            timer.Stop();

            Assert.IsTrue(optimizeTask.IsCompleted, "Optimization did not stop at local maxima");
            Assert.IsTrue(optimizeTask.IsCompletedSuccessfully, "FAILED");

            TestIntegerEvaluableState result = optimizeTask.Result;
            Assert.AreEqual(50, result.Value, "Encountered states do not match");
        }

        public void TestOptimizeEmitsEventsInOrder()
        {
            IComparer<int> comparer = new MaximizingComparer<int>();
            generator = new TestIntegerLocalMaximaSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);

            List<int> encounteredStates = new List<int>();
            List<int> expectedStates = new List<int>();

            for (int i = 3; i <= 50; i++)
            {
                expectedStates.Add(i);
            }

            void OnEvent(object source, ClimberStepEvent<TestIntegerEvaluableState, int> e)
            {
                encounteredStates.Add(e.StepState.Value);
            }

            algorithm.ClimbStepPerformed += OnEvent;

            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(2);
            Task<TestIntegerEvaluableState> optimizeTask = Task.Run(() => algorithm.Optimize(initialState));

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (!optimizeTask.IsCompleted && timer.ElapsedMilliseconds < 10000)
            {
            }

            timer.Stop();

            Assert.IsTrue(optimizeTask.IsCompletedSuccessfully, "FAILED");

            TestIntegerEvaluableState result = optimizeTask.Result;

            Assert.AreEqual(expectedStates.Count, encounteredStates.Count);
            for (int i = 0; i < expectedStates.Count; i++)
            {
                Assert.AreEqual(encounteredStates[i], expectedStates[i], "Encountered states do not match");
            }
        }
    }
}
