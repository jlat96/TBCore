using System;
using System.Collections.Generic;
using TBOptimizer.Climber;
using TBOptimizer.Climber.Events;
using TBOptimizer_CLI_Demo.Evaluation;
using TBOptimizer_CLI_Demo.State;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.Configuration;

namespace TBOptimizer_CLI_Demo
{
    class Program
    {
        static int iterations = 0;
        static void Main(string[] args)
        {
            var climberConfig = new ClimberConfiguration<AddativeMatrixState, int>()
                .ComparesUsing(new MaximizingComparer<int>())
                .GeneratesSuccessorsWith(c =>
                {
                    List<AddativeMatrixState> successors = new List<AddativeMatrixState>();
                    List<AddativeMatrixState> newSwaps;
                    for (int row = 0; row < c.Matrix.GetLength(0); row++)
                    {
                        for (int col = 0; col < c.Matrix.GetLength(1); col++)
                        {
                            newSwaps = c.GetSwappedMatrices(row, col);
                            successors.AddRange(newSwaps);
                        }
                    }
                    return successors;
                });

            IHillClimber<AddativeMatrixState, int> climber = MakeRandomRestartHillClimber(climberConfig);

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
            Console.WriteLine($"Optimized Evaluation after {iterations} iterations: {optimizedState.GetEvaluation()}\n{optimizedState}");
        }

        private static void OnEvent(object s, ClimberStepEvent<AddativeMatrixState, int> e)
        {
            iterations++;
            Console.WriteLine($"Current Evaluation: {e.CurrentState.GetEvaluation()}\n{e.CurrentState.ToString()}\n");
        }

        private static IHillClimber<AddativeMatrixState, int> MakeHillClimber(ClimberConfiguration<AddativeMatrixState, int> config)
        {
            return config.Build();
        }

        private static IHillClimber<AddativeMatrixState, int> MakeRandomRestartHillClimber(ClimberConfiguration<AddativeMatrixState, int> config)
        {
            IHillClimber<AddativeMatrixState, int> climber = new RandomRestartHillClimber<AddativeMatrixState, int>(
                numRestarts: 5,
                climberCofiguration: config,
                randomizationFunction: c =>
                {
                    List<AddativeMatrixState> successors = new List<AddativeMatrixState>();
                    List<AddativeMatrixState> newSwaps;
                    for (int row = 0; row < c.Matrix.GetLength(0); row++)
                    {
                        for (int col = 0; col < c.Matrix.GetLength(1); col++)
                        {
                            newSwaps = c.GetSwappedMatrices(row, col);
                            successors.AddRange(newSwaps);
                        }
                    }

                    int random = new Random().Next(successors.Count - 1);

                    return successors[random];
                });

            return climber;
        }
    }
}
