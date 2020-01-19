using System;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.State;

namespace  OptimizerTests.TestModels.State
{
    public class TestIntegerEvaluableStateNonRandomizer : ISuccessorPicker<TestIntegerEvaluableState, int>
    {
        public TestIntegerEvaluableStateNonRandomizer() 
        {
        }

        /// <summary>
        /// Perform no randomization on the initial state
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public TestIntegerEvaluableState Next(TestIntegerEvaluableState current)
        {
            return current;
        }
    }
}
