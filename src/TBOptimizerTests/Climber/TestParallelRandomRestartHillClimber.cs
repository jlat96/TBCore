using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
using TBOptimizer.Climber;
using TBOptimizer.Climber.Events;
using TBOptimizerTests.TestModels.State;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.Climber
{
    [TestFixture]
    public class TestParallelRandomRestartHillClimber
    {
        private IComparer<int> comparer;
        private ISuccessorGenerator<TestIntegerEvaluableState, int> generator;
        private ClimberSuccessorSelector<TestIntegerEvaluableState, int> picker;
        private LocalClimberAlgorithm<TestIntegerEvaluableState, int> algorithm;
        private IterableHillClimber<TestIntegerEvaluableState, int> climber;
        private ISuccessorSelector<TestIntegerEvaluableState, int> randomizer;

        [Test]
        public void TestRandomRestartHillClimberSameRestartPointEachTime()
        {
            comparer = new MaximizingComparer<int>();
            generator = new TestLinearIntegerSuccessorGenerator();
            picker = new ClimberSuccessorSelector<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);
            randomizer = new TestIntegerEvaluableStateNonRandomizer();
            climber = new ParallelRandomRestartHillClimber<TestIntegerEvaluableState, int>(comparer, generator, randomizer, 5);

            RunTest(climber, 2, 100);
        }

        private void RunTest(IterableHillClimber<TestIntegerEvaluableState, int> climber, int initialStateValue, int expectedOptimalValue)
        {
            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(initialStateValue);
            TestIntegerEvaluableState resultState = new TestIntegerEvaluableState(initialStateValue);

            Dictionary<int, Tuple<TestIntegerEvaluableState, TestIntegerEvaluableState>> localWinners = new Dictionary<int, Tuple<TestIntegerEvaluableState, TestIntegerEvaluableState>>();

            void OnClimberRestartEvent(object source, ClimberCompleteEvent<TestIntegerEvaluableState, int> e)
            {
                lock (localWinners)
                {
                    localWinners[e.CLimberIndex] = new Tuple<TestIntegerEvaluableState, TestIntegerEvaluableState>(e.InitialState, e.OptimizedState);
                }
            }

            climber.ClimberCompleteEvent += OnClimberRestartEvent;

            Stopwatch timer = new Stopwatch();
            Task<TestIntegerEvaluableState> optimizeTask = Task.Run(() => climber.Optimize(initialState));
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
