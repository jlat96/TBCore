using System;
using TBUtils.Types;


namespace TrailBlazerDemo.Card
{
    public class PokerCard : ITypedClonable<PokerCard>, IComparable<PokerCard>
    {
        public static readonly int FACE_ACE = 14;
        public static readonly int FACE_KING = 13;
        public static readonly int FACE_QUEEN = 12;
        public static readonly int FACE_JACK = 11;

        public static readonly int SPADE = 1;
        public static readonly int DIAMOND = 2;
        public static readonly int HEART = 3;
        public static readonly int CLUB = 4;

        public PokerCard() { }

        public int Rank { get; set; }
        public int Suit { get; set; }

        public int Evaluate()
        {
            return Rank;
        }

        public static string DecodeRank(int rank)
        {
            if (rank < 0 || rank > FACE_ACE)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (rank < FACE_JACK)  return rank.ToString();

            if (rank == FACE_JACK)  return "J";

            else if (rank == FACE_QUEEN)  return "Q";

            else if (rank == FACE_KING) return "K";

            return "A";
        }

        public static string DecodeSuit(int suit)
        {
            if (suit < 0 || suit > CLUB)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (suit == SPADE) return "S";
            if (suit == DIAMOND) return "D";
            if (suit == HEART) return "H";
            return "J";
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", DecodeRank(Rank), DecodeSuit(Suit));
        }

        public PokerCard Clone()
        {
            return new PokerCard() { Rank = Rank, Suit = Suit };

        }

        public int CompareTo(PokerCard other)
        {
            int rankCompare = Rank.CompareTo(other.Rank);

            if (rankCompare != 0)
            {
                return Suit.CompareTo(other.Suit) * -1;
            }

            return rankCompare;
        }
    }
}
