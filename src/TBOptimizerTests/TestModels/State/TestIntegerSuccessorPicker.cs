using System;
using System.Collections.Generic;
using OptimizerTests.TestModels.Evaluable;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.Evaluation;
using TrailBlazer.TBOptimizer.State;

namespace OptimizerTests.TestModels.State
{
    public class TestIntegerSuccessorPicker: ISuccessorSelector<TestIntegerEvaluableState, int>
    {

        private readonly IComparer<int> comparer;

        public TestIntegerSuccessorPicker()
        {
            comparer = new MaximizingComparer<int>();
        }

        public TestIntegerEvaluableState Next(TestIntegerEvaluableState current)
        {
            TestIntegerEvaluableState next = MakeNeighbors(current).Extrema(comparer);
            return (next.GetEvaluation() > current.GetEvaluation()) ? next : current;
        }

        /// <summary>
        /// Returns a neighbor to the right and left of the given value up to values of 100.
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private List<TestIntegerEvaluableState> MakeNeighbors(TestIntegerEvaluableState current)
        {
            List<TestIntegerEvaluableState> neighbors = new List<TestIntegerEvaluableState>()
            {
                new TestIntegerEvaluableState(current.Value - 1),
            };

            if (current.Value < 100)
            {
                neighbors.Add(new TestIntegerEvaluableState(current.Value + 1));
            }

            return neighbors;
        }
    }
}
