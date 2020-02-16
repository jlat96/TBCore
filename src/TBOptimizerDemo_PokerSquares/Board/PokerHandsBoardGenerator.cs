using System;
using System.Collections.Generic;
using System.Text;
using TrailBlazer.TBOptimizer.State;

namespace TrailBlazerDemo.Board
{
    public class PokerHandsBoardGenerator : ISuccessorGenerator<PokerSquaresBoard, int>
    {
        public IEnumerable<PokerSquaresBoard> GetSuccessors(PokerSquaresBoard state)
        {
            List<PokerSquaresBoard> successors = new List<PokerSquaresBoard>();
            List<PokerSquaresBoard> newSwaps;
            for (int row = 0; row < PokerSquaresBoard.BOARD_DIMENSION; row++)
            {
                for (int col = 0; col < PokerSquaresBoard.BOARD_DIMENSION; col++)
                {
                    newSwaps = state.GetSwaps(row, col);
                    successors.AddRange(newSwaps);
                }
            }
            return successors;
        }
    }
}
