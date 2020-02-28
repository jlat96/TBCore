using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
using OptimizerTests.TestModels.State;
using TBOptimizer.Climber.Events;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.Climber
{
    [TestFixture]
    public class TestGeneralHillClimber
    {
        private MaximizingComparer<int> comparer;
        private ISuccessorGenerator<TestIntegerEvaluableState, int> generator;
        private ClimberSuccessorSelector<TestIntegerEvaluableState, int> picker;
        private LocalClimberAlgorithm<TestIntegerEvaluableState, int> algorithm;
        private GeneralHillClimber<TestIntegerEvaluableState> climber;

        [SetUp]
        public void Setup()
        {
            comparer = new MaximizingComparer<int>();
            generator = new TestLinearIntegerSuccessorGenerator();
            picker = new ClimberSuccessorSelector<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);
            climber = new GeneralHillClimber<TestIntegerEvaluableState>(algorithm);
        }

        [Test, Timeout(5000)]
        public void TestPerformOptimization()
        {
            TestIntegerEvaluableState initial = new TestIntegerEvaluableState(2);
            TestIntegerEvaluableState result;

            List<TestIntegerEvaluableState> states = new List<TestIntegerEvaluableState>();
            List<TestIntegerEvaluableState> expectedStates = new List<TestIntegerEvaluableState>();
            for (int i = 3; i <= 100; i++)
            {
                expectedStates.Add(new TestIntegerEvaluableState(i));
            }

            void eventCallback(object sender, ClimberStepEvent<TestIntegerEvaluableState, int> args)
            {
                states.Add(args.CurrentState);
            };

            climber.ClimberStepPerformedEvent += eventCallback;

            result = climber.Optimize(initial);

            Assert.AreEqual(100, result.Value);
            Assert.AreEqual(expectedStates.Count, states.Count);

            for (int i = 0; i < states.Count; i++)
            {
                Assert.IsTrue(states[i].CompareTo(expectedStates[i]) == 0);
            }
        }

        [Test, Timeout(5000)]
        public void TestPerformOptimizationReturnsLocalExtrema()
        {
            TestIntegerEvaluableState initial = new TestIntegerEvaluableState(2);
            TestIntegerEvaluableState result;

            generator = new TestIntegerLocalMaximaSuccessorGenerator();
            picker = new ClimberSuccessorSelector<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);
            climber = new GeneralHillClimber<TestIntegerEvaluableState>(algorithm);

            result = climber.Optimize(initial);

            Assert.AreEqual(50, result.Value);
        }
    }
}
