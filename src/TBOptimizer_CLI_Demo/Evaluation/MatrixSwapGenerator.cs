using System;
using System.Collections.Generic;
using TBOptimizer_CLI_Demo.State;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer_CLI_Demo.Evaluation
{
    public class MatrixSwapGenerator : ISuccessorGenerator<AddativeMatrixState, int>
    {
        public IEnumerable<AddativeMatrixState> GetSuccessors(AddativeMatrixState state)
        {
            List<AddativeMatrixState> successors = new List<AddativeMatrixState>();
            List<AddativeMatrixState> newSwaps;
            for (int row = 0; row < state.Matrix.GetLength(0); row++)
            {
                for (int col = 0; col < state.Matrix.GetLength(1); col++)
                {
                    newSwaps = state.GetSwappedMatrices(row, col);
                    successors.AddRange(newSwaps);
                }
            }
            return successors;
        }
    }
}
