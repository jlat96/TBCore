using System;
using System.Collections.Generic;
using TBUtils.Types;

namespace TrailBlazerDemo.Card
{
    public class CardDeck : ITypedClonable<CardDeck>
    {
        public static readonly int SHUFFLE_ITERATIONS = 100;
        public static readonly List<int> SUITS = new List<int>() { PokerCard.SPADE, PokerCard.DIAMOND, PokerCard.HEART, PokerCard.CLUB };

        private static readonly int DEFAULT_CARDS_PER_SUIT = 14;

        private List<PokerCard> cards;
        private int positionInDeck;

        public CardDeck(List<PokerCard> cards)
        {
            this.cards = cards;
            this.positionInDeck = 0;
        }

        public static CardDeck CreateDefaultDeck()
        {
            List<PokerCard> cards = new List<PokerCard>();
            foreach (int suit in SUITS)
            {
                for (int rank = 2; rank < DEFAULT_CARDS_PER_SUIT + 1; rank++)
                {
                    cards.Add(new PokerCard
                    {
                        Rank = rank,
                        Suit = suit,
                    }); 
                }
            }
            return new CardDeck(cards);
        }

        public CardDeck Clone()
        {
            return new CardDeck(cards);
        }

        public List<PokerCard> Deal(int num)
        {
            List<PokerCard> hand = new List<PokerCard>();
            for (int i = 0; i < num; i++)
            {
                if (positionInDeck > cards.Count)
                {
                    throw new IndexOutOfRangeException("Deck out of cards");
                }
                hand.Add(cards[positionInDeck++]);
            }

            return hand;
        }

        public void Shuffle()
        {
            Random numberGenerator = new Random();

            int x = -1;
            int y = -1;
            PokerCard temp;

            for (int i = 0; i < SHUFFLE_ITERATIONS; i++)
            {
                x = numberGenerator.Next(0, cards.Count);
                y = numberGenerator.Next(0, cards.Count);

                temp = cards[x];
                cards[x] = cards[y];
                cards[y] = temp;
            }
        }
    }
}
