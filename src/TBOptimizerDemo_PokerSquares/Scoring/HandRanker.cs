using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrailBlazerDemo.Card;

namespace TrailBlazerDemo.Scoring
{
    public class HandRanker : IComparer<ScoreResult>
    {
        private readonly List<Func<List<PokerCard>, ScoreResult>> scoringFunctions;

        public readonly static int NUM_HAND_TYPES = 9;
        public readonly static int STRAIGHT_FLUSH = 8;
        public readonly static int FOUR_OF_A_KIND = 7;
        public readonly static int FULL_HOUSE = 6;
        public readonly static int FLUSH = 5;
        public readonly static int STRAIGHT = 4;
        public readonly static int THREE_OF_A_KIND = 3;
        public readonly static int TWO_PAIR = 2;
        public readonly static int PAIR = 1;
        public readonly static int HIGH_CARD = 0;

        public HandRanker()
        {
            scoringFunctions = new List<Func<List<PokerCard>, ScoreResult>>()
            {
                CheckStraightFlush,
                CheckFourOfAKind,
                CheckFullHouse,
                CheckFlush,
                CheckStraight,
                CheckThreeOfAKind,
                CheckTwoPair,
                CheckPair,
                CheckHighCard
            };
        }

        public ScoreResult ScoreHand(List<PokerCard> cards)
        {
            return ScoreHand(new CardHand(cards));
        }

        public ScoreResult ScoreHand(CardHand hand)
        {
            CardHand sortedHand = new CardHand(hand.GetCards().OrderByDescending(c => c.Rank).ToList());

            ScoreResult rank = new ScoreResult() { };

            // Return first highest score result
            foreach (Func<List<PokerCard>, ScoreResult> a in scoringFunctions)
            {
                rank = a(sortedHand.Cards);
                if (rank.Score != 0)
                {
                    return rank;
                }
            }

            // return HIGH_CARD
            return rank;
        }

        private ScoreResult CheckHighCard(List<PokerCard> cards)
        {
            return new ScoreResult() { Score = HIGH_CARD, Cards = cards};
        }
        private ScoreResult CheckPair(List<PokerCard> cards)
        {
            int pair = 0;
            int index = cards.Count - 1;
            List<PokerCard> pairs = new List<PokerCard>();
            List<PokerCard> highCards = new List<PokerCard>();

            while (index > 0)
            {
                if (cards[index].Rank == cards[index - 1].Rank)
                {
                    pair = PAIR;
                    pairs.Append(cards[index]);
                    index -= 2;
                }
                else
                {
                    highCards.Append(cards[index]);
                    index--;
                }
            }

            pairs.AddRange(highCards);

            return new ScoreResult() { Score = pair, Cards = pairs };
        }

        private ScoreResult CheckTwoPair(List<PokerCard> cards)
        {
            return new ScoreResult() { Score = 0, Cards = cards };
        }

        private ScoreResult CheckThreeOfAKind(List<PokerCard> cards)
        {
            int threeOfAKind = 0;
            List<PokerCard> result = new List<PokerCard>();

            for (int i = 0; i < cards.Count - 2; i++)
            {
                if (cards[i].Rank == cards[i + 1].Rank && cards[i].Rank == cards[i + 2].Rank)
                {
                    threeOfAKind = THREE_OF_A_KIND;
                    result.Add(cards[i]);
                    for (int j = cards.Count - 1; j > 0; j--)
                    {
                        if (cards[j].Rank != cards[i].Rank)
                        {
                            result.Append(cards[j]);
                        }
                    }
                    break;
                }
            }

            return new ScoreResult() { Score = threeOfAKind, Cards = result };
        }

        private ScoreResult CheckFourOfAKind(List<PokerCard> cards)
        {
            int fourOfAKind = 0;
            List<PokerCard> result = new List<PokerCard>();
            PokerCard kicker;
            
            for (int i = 0; i < 2; i++)
            {
                if (cards[i].Rank == cards[1].Rank && cards[i].Rank == cards[i + 2].Rank && cards[i].Rank == cards[i + 3].Rank)
                {
                    fourOfAKind = FOUR_OF_A_KIND;
                    result.Add(cards[i]);
                    if (i == 0)
                    {
                        kicker = cards[4];
                    }
                    else
                    {
                        kicker = cards[0];
                    }
                    result.Add(kicker);
                    break;
                }
            }

            return new ScoreResult() { Score = fourOfAKind, Cards = result };
        }

        private ScoreResult CheckFullHouse(List<PokerCard> cards)
        {
            int fullHouse = 0;
            List<PokerCard> result = new List<PokerCard>();
            if (CheckPair(cards.GetRange(0, 2)).Score != 0 
                && CheckThreeOfAKind(cards.GetRange(2, 3)).Score != 0)
            {
                fullHouse = FULL_HOUSE;
                result = new List<PokerCard>() { cards[2], cards[0] };
            }
            else if (CheckThreeOfAKind(cards.GetRange(0, 3)).Score != 0 
                && CheckPair(cards.GetRange(3, 2)).Score != 0)
            {
                fullHouse = FULL_HOUSE;
                result = new List<PokerCard>() { cards[0], cards[3] };
            }

            return new ScoreResult() { Score = fullHouse, Cards = result };
        }

        private ScoreResult CheckStraight(List<PokerCard> cards)
        {
            int _straight = STRAIGHT;
            int lastIndex = 0;
            PokerCard highCard = new PokerCard() { Rank = 0, Suit = 0 };

            for (int i = 1; i < cards.Count; i++)
            {
                if (cards[i - 1].Rank + 1 != cards[i].Rank)
                {
                    _straight = 0;
                    break;
                }
                lastIndex = i;
            }
            
            // Special case: Ace low/high
            if (lastIndex == 3 
                && cards[lastIndex + 1].Rank == PokerCard.FACE_ACE 
                && _straight == 0 
                && cards[0].Rank == 2)
            {
                _straight = STRAIGHT;
                highCard = cards[lastIndex];
            }
            else if (_straight != 0)
            {
                highCard = cards[cards.Count - 1];
            }

            return new ScoreResult() { Score = _straight, Cards = new List<PokerCard>() { highCard } };
        }

        private ScoreResult CheckFlush(List<PokerCard> cards)
        {
            int _flush = FLUSH;
            IEnumerable<PokerCard> result = new List<PokerCard>();
            foreach (PokerCard card in cards)
            {
                if (cards[0].Suit != card.Suit)
                {
                    _flush = 0;
                    break;
                }
            }

            result = cards.OrderBy(c => c.Rank);

            return new ScoreResult() { Score = _flush, Cards = result.ToList() };
        }

        private ScoreResult CheckStraightFlush(List<PokerCard> cards)
        {
            ScoreResult straightTest;
            if (CheckFlush(cards).Score != 0)
            {
                straightTest = CheckStraight(cards);
                if (straightTest.Score != 0)
                {
                    return new ScoreResult()
                    {
                        Score = STRAIGHT_FLUSH,
                        Cards = new List<PokerCard>() { straightTest.Cards[1] }
                    };
                }
            }
            return new ScoreResult() { Score = 0, Cards = cards };
        }

        public int Compare(ScoreResult x, ScoreResult y)
        {
            if (x.Score > y.Score) return 1;
            if (x.Score < y.Score) return -1;

            // Compare each respective card in the tied ScoreResults card list
            // TODO Test this
            foreach (var tieResult in x.Cards.Zip(y.Cards, (a, b) => new Tuple<PokerCard, PokerCard>(a, b)))
            {
                if (tieResult.Item1.Rank > tieResult.Item2.Rank) return 1;
                if (tieResult.Item1.Rank < tieResult.Item2.Rank) return -1;
            }
                
            return 0;
        }
    }
}
