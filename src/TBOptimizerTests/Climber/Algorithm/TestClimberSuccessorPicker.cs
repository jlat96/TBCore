using System;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.Climber.Algorithm
{
    [TestFixture]
    public class TestClimberSuccessorPicker
    {
        private ISuccessorGenerator<TestIntegerEvaluableState, int> generator;
        private ClimberSuccessorPicker<TestIntegerEvaluableState, int> picker;

        [SetUp]
        public void Setup()
        {
            generator = new TestIntegerSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, new MaximizingComparer<int>());
        }

        [Test]
        public void TestNext()
        {
            TestIntegerEvaluableState initial = new TestIntegerEvaluableState(1);
            TestIntegerEvaluableState result = picker.Next(initial);

            Assert.AreEqual(2, result.Value);
        }

        [Test]
        public void TestNextIsEqualToCurrent()
        {
            TestIntegerEvaluableState initial = new TestIntegerEvaluableState(100);
            TestIntegerEvaluableState result = picker.Next(initial);

            Assert.AreEqual(100, result.Value);
            Assert.AreEqual(initial, result);
        }
    }
}
