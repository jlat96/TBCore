using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
using TBOptimizerTests.TestModels.State;
using TrailBlazer.TBOptimizer;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.Climber
{
    [TestFixture]
    public class TestRandomRestartHillClimber
    {
        private IComparer<int> comparer;
        private ISuccessorGenerator<TestIntegerEvaluableState, int> generator;
        private ClimberSuccessorPicker<TestIntegerEvaluableState, int> picker;
        private LocalClimberAlgorithm<TestIntegerEvaluableState, int> algorithm;
        private Optimizer<TestIntegerEvaluableState, int> climber;
        private ISuccessorPicker<TestIntegerEvaluableState, int> randomizer;

        [Test]
        public void TestRandomRestartHillClimberSameRestartPointEachTime()
        {
            comparer = new MaximizingComparer<int>();
            generator = new TestLinearIntegerSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);
            randomizer = new TestIntegerEvaluableStateNonRandomizer();
            climber = new RandomRestartHillClimber<TestIntegerEvaluableState, int>(randomizer, algorithm, 5);

            RunTest(climber, 2, 100);
        }

        [Test]
        public void TestRandomRestartHillClimberIncrementingRestartPoint()
        {
            comparer = new MaximizingComparer<int>();
            generator = new TestExponentialIntegerSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);
            randomizer = new TestIntegerRandomizerSimulator();
            climber = new RandomRestartHillClimber<TestIntegerEvaluableState, int>(randomizer, algorithm, 5);


            RunTest(climber, 1, 64);
        }

        private void RunTest(Optimizer<TestIntegerEvaluableState, int> climber, int initialStateValue, int expectedOptimalValue)
        {
            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(initialStateValue);
            TestIntegerEvaluableState resultState = new TestIntegerEvaluableState(initialStateValue);

            Stopwatch timer = new Stopwatch();
            Task<TestIntegerEvaluableState> optimizeTask = Task.Run(() => climber.PerformOptimization(initialState));
            timer.Start();

            while (!optimizeTask.IsCompleted && timer.ElapsedMilliseconds < 20000)
            {
            }

            timer.Stop();

            Assert.IsTrue(optimizeTask.IsCompleted, "Operation took too long to complete");
            Assert.IsTrue(optimizeTask.IsCompletedSuccessfully, "Operation failed");

            resultState = optimizeTask.Result;

            Assert.AreEqual(expectedOptimalValue, resultState.Value);
        }
    }
}
