using System;
using System.Collections.Generic;
using System.Text;
using TrailBlazerDemo.Card;

namespace TrailBlazerDemo.Scoring
{
    public class ScoreResult
    {
        public int Score { get; set; } = 0;
        public List<PokerCard> Cards { get; set; } = new List<PokerCard>();
    }
}
