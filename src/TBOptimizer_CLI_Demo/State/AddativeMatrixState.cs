using System;
using System.Collections.Generic;
using System.Text;
using TrailBlazer.TBOptimizer.State;

namespace TBOptimizer_CLI_Demo.State
{
    public class AddativeMatrixState : EvaluableState<AddativeMatrixState, int>
    {

        public AddativeMatrixState(int[,] matrix)
        {
            Matrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Matrix[row, col] = matrix[row, col];
                }
            }
        }

        public override AddativeMatrixState Clone()
        {
            return new AddativeMatrixState(Matrix);
        }

        public readonly int[,] Matrix;

        protected override int Evaluate()
        {
            int horizontalSum = 0;

            for (int row = 0; row < Matrix.GetLength(0); row++)
            {
                for (int col = 0; col < Matrix.GetLength(1); col++)
                {
                    horizontalSum += GetIndexScore(row, col);
                }
            }

            return horizontalSum;
        }

        private int GetIndexScore(int x, int y)
        {
            int current = Matrix[x, y];
            int multiplier;

            if (y > 0)
            {
                multiplier = Matrix[x, y - 1];
            }
            else
            {
                multiplier = 1;
            }

            return current * multiplier;
        }

        public List<AddativeMatrixState> GetSwappedMatrices(int rowToSwap, int colToSwap)
        {
            List<AddativeMatrixState> swappedBoards = new List<AddativeMatrixState>();
            AddativeMatrixState newMatrix;
            int temp;
            for (int row = 0; row < Matrix.GetLength(0); row++)
            {
                for (int col = 0; col < Matrix.GetLength(1); col++)
                {
                    newMatrix = Clone();
                    temp = newMatrix.Matrix[row, col];
                    newMatrix.Matrix[row, col] = newMatrix.Matrix[rowToSwap, colToSwap];
                    newMatrix.Matrix[rowToSwap, colToSwap] = temp;

                    swappedBoards.Add(newMatrix);
                }
            }

            return swappedBoards;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int row = 0; row < Matrix.GetLength(0); row++)
            {
                for (int col = 0; col < Matrix.GetLength(1); col++)
                {
                    builder.Append($"{Matrix[row, col].ToString()} ");
                }
                builder.AppendLine();
            }

            return builder.ToString().Trim();
        }
    }
}
