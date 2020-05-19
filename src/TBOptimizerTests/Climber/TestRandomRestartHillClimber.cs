using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
using TBOptimizer.Climber.Events;
using TBOptimizerTests.TestModels.State;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.Configuration;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.Climber
{
    [TestFixture]
    public class TestRandomRestartHillClimber
    {
        private IComparer<int> comparer;
        private ISuccessorGenerator<TestIntegerEvaluableState, int> generator;
        private RandomRestartHillClimber<TestIntegerEvaluableState, int> climber;
        private ISuccessorSelector<TestIntegerEvaluableState, int> randomizer;

        [Test]
        public void TestRandomRestartHillClimberSameRestartPointEachTime()
        {
            comparer = new MaximizingComparer<int>();
            generator = new TestLinearIntegerSuccessorGenerator();
            randomizer = new TestIntegerEvaluableStateNonRandomizer();

            var climberConfiguration = new ClimberConfiguration<TestIntegerEvaluableState, int>()
                .ComparesUsing(comparer)
                .GeneratesSuccessorsWith((c) => generator.GetSuccessors(c));

            randomizer = new TestIntegerEvaluableStateNonRandomizer();
            climber = new RandomRestartHillClimber<TestIntegerEvaluableState, int>(5, randomizer, climberConfiguration);

            RunTest(climber, 2, 100);
        }

        [Test, Timeout(20000)]
        public void TestRandomRestartHillClimberIncrementingRestartPoint()
        {
            comparer = new MaximizingComparer<int>();
            generator = new TestExponentialIntegerSuccessorGenerator();

            var climberConfiguration = new ClimberConfiguration<TestIntegerEvaluableState, int>()
                .ComparesUsing(comparer)
                .GeneratesSuccessorsWith((c) => generator.GetSuccessors(c));

            randomizer = new TestIntegerRandomizerSimulator();
            climber = new RandomRestartHillClimber<TestIntegerEvaluableState, int>(5, randomizer, climberConfiguration);

            RunTest(climber, 1, 10000);
        }

        private void RunTest(RandomRestartHillClimber<TestIntegerEvaluableState, int> climber, int initialStateValue, int expectedOptimalValue)
        {
            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(initialStateValue);
            TestIntegerEvaluableState resultState = new TestIntegerEvaluableState(initialStateValue);


            Dictionary<int, Tuple<TestIntegerEvaluableState, TestIntegerEvaluableState>> localWinners = new Dictionary<int, Tuple<TestIntegerEvaluableState, TestIntegerEvaluableState>>();

            void OnClimberRestartEvent(object source, ClimberCompleteEvent<TestIntegerEvaluableState, int> e)
            {
                localWinners[e.CLimberIndex] = new Tuple<TestIntegerEvaluableState, TestIntegerEvaluableState>(e.InitialState, e.OptimizedState);
            }

            climber.ClimberCompleteEvent += OnClimberRestartEvent;

            resultState = climber.Optimize(initialState);

            Assert.AreEqual(expectedOptimalValue, resultState.Value);
        }
    }
}
