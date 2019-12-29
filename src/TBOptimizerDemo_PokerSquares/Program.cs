using System;
using System.Collections.Generic;
using System.Linq;
using TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer;
using TrailBlazer.TBOptimizer.Climber;
using TrailBlazer.TBOptimizer.Climber.Algorithm;
using TrailBlazer.TBOptimizer.Comparison;
using TrailBlazer.TBOptimizer.State;
using TrailBlazerDemo.Board;
using TrailBlazerDemo.Card;
using TrailBlazerDemo.Game;

namespace TrailBlazerDemo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            bool greedy = false;
            bool random = false;

            Optimizer<PokerSquaresBoard, int> climber;
            ISuccessorGenerator<PokerSquaresBoard, int> boardGenerator = new PokerHandsBoardGenerator();
            IComparer<int> boardComparer = new MaximizingComparer<int>();
            ISuccessorPicker<PokerSquaresBoard, int> boardPicker = new ClimberSuccessorPicker<PokerSquaresBoard, int>(boardGenerator, boardComparer);
            NumericClimberAlgorithm<PokerSquaresBoard> algorithm = new NumericClimberAlgorithm<PokerSquaresBoard>(boardComparer, boardPicker);

            if (args.Length < 2)
            {
                climber = new GeneralHillClimber<PokerSquaresBoard>(algorithm);
            }
            else
            {
                foreach (string arg in args.Skip(1))
                {
                    switch (arg)
                    {
                        case "-g":
                            goto case "--greedy";
                        case "--greedy":
                            greedy = true;
                            break;
                        case "-r":
                            goto case "--random";
                        case "--random":
                            random = true;
                            break;
                        default: break;
                    }
                }

                algorithm = new NumericClimberAlgorithm<PokerSquaresBoard>(greedy, boardComparer, boardPicker);
                climber = new GeneralHillClimber<PokerSquaresBoard>(algorithm);
            }
            CardDeck deck = CardDeck.CreateDefaultDeck();
            deck.Shuffle();
            PokerSquaresGame game = new PokerSquaresGame(climber);
            Console.WriteLine(game.InitialBoard.ToString() + "\n");

            PokerSquaresBoard bestBoard = game.OptimizeBoard();
            Console.WriteLine(bestBoard.ToString());
        }
    }
}
