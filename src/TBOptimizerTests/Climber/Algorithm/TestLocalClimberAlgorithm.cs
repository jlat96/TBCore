using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
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
            generator = new TestIntegerSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, new MaximizingComparer<int>());
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(new MaximizingComparer<int>(), picker);
        }

        [Test]
        public void TestOptimize()
        {
            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(2);

            Task<TestIntegerEvaluableState> task = Task.Run(() => algorithm.Optimize(initialState));

            Stopwatch timer = new Stopwatch();
            timer.Start();

            bool complete = false;

            while (!complete && timer.ElapsedMilliseconds < 5000)
            {
                complete = task.IsCompleted;
            }
            timer.Stop();

            Assert.IsTrue(complete, "Optimization took too long to complete");

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
            bool complete = false;
            while(!complete && timer.ElapsedMilliseconds < 5000)
            {
                complete = optimizeTask.IsCompleted;
            }

            timer.Stop();

            Assert.IsTrue(complete, "Optimization did not stop at local maxima");
            TestIntegerEvaluableState result = optimizeTask.Result;

            Assert.AreEqual(50, result.Value);
        }
    }
}
