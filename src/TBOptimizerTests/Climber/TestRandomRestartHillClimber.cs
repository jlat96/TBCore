﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using OptimizerTests.TestModels.Evaluable;
using OptimizerTests.TestModels.State;
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
            generator = new TestIntegerSuccessorGenerator();
            picker = new ClimberSuccessorPicker<TestIntegerEvaluableState, int>(generator, comparer);
            algorithm = new LocalClimberAlgorithm<TestIntegerEvaluableState, int>(comparer, picker);
            randomizer = new TestIntegerEvaluableStateNonRandomizer();
            climber = new RandomRestartHillClimber<TestIntegerEvaluableState, int>(randomizer, algorithm, 5);

            TestIntegerEvaluableState initialState = new TestIntegerEvaluableState(2);
            TestIntegerEvaluableState resultState = new TestIntegerEvaluableState(2);

            Stopwatch timer = new Stopwatch();
            Task<TestIntegerEvaluableState> optimizeTask = Task.Run(() => climber.PerformOptimization(initialState));
            timer.Start();

            while (!optimizeTask.IsCompleted && timer.ElapsedMilliseconds < 20000)
            {
            }

            timer.Stop();

            Assert.IsTrue(optimizeTask.IsCompleted, "Operation took too long to complete");

            resultState = optimizeTask.Result;

            Assert.AreEqual(100, resultState.Value);
        }
    }
}