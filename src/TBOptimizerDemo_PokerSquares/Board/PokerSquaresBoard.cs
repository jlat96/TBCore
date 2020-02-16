using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrailBlazer.TBOptimizer.State;
using TrailBlazerDemo.Card;
using TrailBlazerDemo.Scoring;
using TrailBlazerDemo.Utilities;

namespace TrailBlazerDemo.Board
{
    public class PokerSquaresBoard : EvaluableState<PokerSquaresBoard, int>
    {
        public static readonly int BOARD_SIZE = 25;
        public static readonly int BOARD_DIMENSION = 5;

        public static readonly int STRAIGHT_FLUSH = 30;
        public static readonly int FOUR_OF_A_KIND = 30;
        public static readonly int FULL_HOUSE = 10;
        public static readonly int FLUSH = 5;
        public static readonly int STRAIGHT = 12;
        public static readonly int THREE_OF_A_KIND = 6;
        public static readonly int TWO_PAIR = 3;
        public static readonly int PAIR = 1;
        public static readonly int HIGH_CARD = 0;

        private static readonly List<int> SCORES = new List<int>()
        {
            STRAIGHT_FLUSH,
            FOUR_OF_A_KIND,
            FULL_HOUSE,
            FLUSH,
            STRAIGHT,
            THREE_OF_A_KIND,
            TWO_PAIR,
            PAIR,
            HIGH_CARD
        };

        private readonly CardDeck deck;
        private readonly List<PokerCard> tableCards;

        public PokerSquaresBoard(CardDeck deck)
        {
            this.deck = deck;

            tableCards = deck.Deal(BOARD_SIZE);
            Board = CreateBoardFromCards(tableCards);
        }

        public PokerCard[,] Board { get; }

        protected override int Evaluate()
        {
            List<int> scores = new List<int>();
            for (int row = 0; row < BOARD_DIMENSION; row++)
            {
                scores.Add(ScoreRow(row));
            }

            for (int col = 0; col < BOARD_DIMENSION; col++)
            {
                scores.Add(ScoreCol(col));
            }

            return scores.Sum();
        }

        public PokerSquaresBoard Shuffle()
        {
            CardDeck newDeck = deck.Clone();
            newDeck.Shuffle();
            return new PokerSquaresBoard(newDeck);
        }

        public List<PokerSquaresBoard> GetSwaps(int rowToSwap, int colToSwap)
        {
            List<PokerSquaresBoard> swappedBoards = new List<PokerSquaresBoard>();
            PokerSquaresBoard newBoard;
            PokerCard temp;
            for (int row = 0; row < BOARD_DIMENSION; row++)
            {
                for (int col = 0; col < BOARD_DIMENSION; col++)
                {
                    newBoard = Clone();
                    temp = newBoard.Board[row, col].Clone();
                    newBoard.Board[row, col] = newBoard.Board[rowToSwap, colToSwap];
                    newBoard.Board[rowToSwap, colToSwap] = temp;

                    swappedBoards.Add(newBoard);
                }
            }

            return swappedBoards;
        }

        private int ScoreRow(int rowIndex)
        {
            IEnumerable<PokerCard> rowToScore = Board.SliceRow(rowIndex);
            int handResult = new CardHand(rowToScore).GetEvaluation();
            return SCORES[handResult];
        }

        private int ScoreCol(int colIndex)
        {
            IEnumerable<PokerCard> rowToScore = Board.SliceCol(colIndex);
            int handResult = new CardHand(rowToScore).GetEvaluation();
            return SCORES[handResult];
        }

        private static PokerCard[,] CreateBoardFromCards(List<PokerCard> cards)
        {
            int xyLength = BOARD_SIZE / BOARD_DIMENSION;
            PokerCard[,] board = new PokerCard[xyLength, xyLength];
            int cardIndex = 0;
            for (int i = 0; i < xyLength; i++)
            {
                for (int j = 0; j < xyLength; j++)
                {
                    board[i, j] = cards[cardIndex++];
                }
            }
            return board;
        }

        public override string ToString()
        {
            StringBuilder boardString = new StringBuilder();
            for (int row = 0; row < BOARD_DIMENSION; row++)
            {
                for (int col = 0; col < BOARD_DIMENSION; col++)
                {
                    boardString.Append(Board[row, col] + "\t");
                }
                boardString.Append(ScoreRow(row) + "\n");
            }

            for (int col = 0; col < BOARD_DIMENSION; col++)
            {
                boardString.Append(ScoreCol(col) + "\t");
            }

            boardString.Append(Evaluate());

            return boardString.ToString();
        }

        public override PokerSquaresBoard Clone()
        {
            return new PokerSquaresBoard(deck.Clone());
        }
    }
}
