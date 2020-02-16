using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrailBlazer.TBOptimizer.State;
using TrailBlazerDemo.Scoring;

namespace TrailBlazerDemo.Card
{
    public class CardHand : EvaluableState<CardHand, int>
    {
        private readonly HandRanker ranker;

        public CardHand(IEnumerable<PokerCard> cards)
        {
            Cards = cards.ToList();
            ranker = new HandRanker();
        }

        public List<PokerCard> Cards { get; }

        public List<PokerCard> GetCards()
        {
            return Cards;
        }

        protected override int Evaluate()
        {
            return ranker.ScoreHand(this).Score;
        }

        public override string ToString()
        {
            StringBuilder handString = new StringBuilder();
            foreach (PokerCard c in Cards)
            {
                handString.Append(string.Format("{0} ", c.ToString()));
            }
            return handString.ToString().Trim();
        }

        /// <summary>
        /// Compare this card hand to the other card hand. If the hands have the
        /// same score, attempt to break the tie
        /// </summary>
        /// <param name="other">The hand to compare to</param>
        /// <returns>Negative if this is evaluates to less than other, 0 if
        /// this is equal to other and the tie cannot be broken, positive if
        /// this is greater than other</returns>
        public override int CompareTo(CardHand other)
        {
            if (Evaluation != other.Evaluation)
            {
                return Evaluation.CompareTo(other.Evaluation);
            }

            // else break tie

            return 0;
        }

        public override CardHand Clone()
        {
            return new CardHand(Cards);
        }
    }
}
