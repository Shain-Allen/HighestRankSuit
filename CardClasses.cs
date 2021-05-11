using System;

namespace HighestRankSuit
{
    public enum Suit { Clubs, Diamonds, Hearts, Spades };
    public enum Rank { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };

    // -------------------------------------------------------------------------------------------

    public class Card
    {
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }

        public Card(Rank rank = Rank.Ace, Suit suit = Suit.Spades)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString()
        {
            return $"[{Rank} of {Suit}]";
        }

    }

    // -------------------------------------------------------------------------------------------

    public class Deck
    {
        private Card[] cards;

        public Deck()
        {
            Array suits = Enum.GetValues(typeof(Suit));
            Array ranks = Enum.GetValues(typeof(Rank));

            cards = new Card[suits.Length * ranks.Length];

            int i = 0;
            foreach (Suit suit in suits)
            {
                foreach (Rank rank in ranks)
                {
                    Card card = new Card(rank, suit);
                    cards[i++] = card;
                }
            }
        }

        public int Size()
        {
            return cards.Length;
        }

        public void Shuffle()
        {
            Random rng = new Random();

            int deckSize = Size();
            if (deckSize == 0) return;                    // Cannot shuffle an empty deck

            // Fisher-Yates Shuffle (modern algorithm)
            //   - http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
            for (int i = 0; i < deckSize; i++)
            {
                int j = rng.Next(i, deckSize);
                Card c = cards[i];
                cards[i] = cards[j];
                cards[j] = c;
            }
        }

        public Card DealCard()
        {
            if (Size() == 0) return null;

            Card card = cards[Size() - 1];
            Array.Resize(ref cards, Size() - 1);
            return card;
        }

        public override string ToString()
        {
            string s = "[";
            string comma = "";
            foreach (Card c in cards)
            {
                s += comma + c.ToString();
                comma = ", ";
            }
            s += "]";
            s += "\n " + Size() + " cards in deck.\n";

            return s;
        }

    }
}