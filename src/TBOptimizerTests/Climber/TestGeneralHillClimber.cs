using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;

namespace OptimizerTests.Climber
{
    [TestFixture]
    public class TestGeneralHillClimber
    {
        private MaximizingComparer<int> comparer;
        private TestIntegerSuccessorGenerator generator;
        private ClimberSuccessorPicker<TestIntegerEvaluableState, int> picker;
        private LocalClimberAlgorithm<TestIntegerEvaluableState, int> algorithm;
        private GeneralHillClimber<TestIntegerEvaluableState> climber;

        [SetUp]
        public void Setup()
        {
            comparer = new MaximizingComparer<int>();
            generator = new TestIntegerSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);
            climber = new GeneralHillClimber<TestIntegerEvaluableState>(algorithm);
        }

        [Test]
        public void TestPerformOptiomization()
        {
            TestIntegerEvaluableState initial = new TestIntegerEvaluableState(2);
            TestIntegerEvaluableState result;

            Task<TestIntegerEvaluableState> optimizeTask = Task.Run(() => climber.PerformOptimization(initial));

            bool complete = false;
            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (!complete && timer.ElapsedMilliseconds < 5000)
            {
                complete = optimizeTask.IsCompleted;
            }

            timer.Stop();
            Assert.IsTrue(complete, "Optimization exceeded time limit");

            result = optimizeTask.Result;

            Assert.AreEqual(100, result.Value);

        }

        
    }
}
