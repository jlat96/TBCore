using TrailBlazer.TBOptimizer;
using TrailBlazerDemo.Board;
using TrailBlazerDemo.Card;

namespace TrailBlazerDemo.Game
{
    public class PokerSquaresGame
    {
        private readonly IOptimizer<PokerSquaresBoard, int> optimizer;

        public PokerSquaresGame(IOptimizer<PokerSquaresBoard, int> climber) : this(climber, new PokerSquaresBoard(CardDeck.CreateDefaultDeck())) { }

        public PokerSquaresGame(IOptimizer<PokerSquaresBoard, int> optimizer, PokerSquaresBoard board)
        {
            InitialBoard = board;
            this.optimizer = optimizer;
        }

        public PokerSquaresBoard InitialBoard { get; }

        public PokerSquaresBoard OptimizeBoard()
        {
            return optimizer.PerformOptimization(InitialBoard);
        }
    }
}
