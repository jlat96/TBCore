using System;
using System.Collections.Generic;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.State;
using TrailBlazerDemo.Board;

namespace OprimizerDemo.Board
{
    public class BoardPicker : ClimberSuccessorPicker<PokerSquaresBoard, int>
    {

        public BoardPicker(ISuccessorGenerator<PokerSquaresBoard, int> boardGenerator, IComparer<int> comparer) : base(boardGenerator, comparer)
        {

        }
    }
}
