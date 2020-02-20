using System;
using System.Collections.Generic;
using TBOptimizer.Climber.Events;
using TBOptimizer_CLI_Demo.Evaluation;
using TBOptimizer_CLI_Demo.State;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Comparison;

namespace TBOptimizer_CLI_Demo
{
    class Program
    {
        static int iterations = 0;
        static void Main(string[] args)
        {
            IComparer<int> comparer = new MaximizingComparer<int>();
            HillClimber<AddativeMatrixState, int> climber = new HillClimber<AddativeMatrixState, int>(comparer, new MatrixSwapGenerator());

            climber.ClimberStepPerformedEvent += OnEvent;

            int[,] initialMatrix = new int[,]
            {
                { 1,2,3,4,5},
                { 6,7,8,9,10},
                { 1,2,3,4,5},
                { 6,7,8,9,1},
                { 10,2,3,4,5},
            };

            AddativeMatrixState initialState = new AddativeMatrixState(initialMatrix);

            AddativeMatrixState optimizedState = climber.Optimize(initialState);
            Console.WriteLine($"Optimized Evaluation after {iterations} iterations: {optimizedState.GetEvaluation()}\n{optimizedState.ToString()}");
        }

        private static void OnEvent(object s, ClimberStepEvent<AddativeMatrixState, int> e)
        {
            iterations++;
            Console.WriteLine($"Current Evaluation: {e.StepState.GetEvaluation()}\n{e.StepState.ToString()}\n");
        }
    }
}
