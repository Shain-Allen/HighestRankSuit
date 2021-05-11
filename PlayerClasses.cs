using System;

// Uses Automatic .NET Properties

namespace HighestRankSuit
{
    public class Player
    {
        public string Name { get; private set; }
        public int Score { get; private set; }
        private Hand hand;          // No getters/setters for hand, so use a private member variable

        public Player(string name)
        {
            Name = name;
            Score = 0;
            hand = new Hand();
        }

        public void AddCardToHand(Card card)
        {
            if (card == null)
                return;

            hand.AddCard(card);
        }

        public Card RemoveCardFromHand()
        {
            return hand.RemoveRandomCard();
        }

        public void AddPoint()
        {
            Score++;
        }

        public override string ToString()
        {
            return $"{Name}'s Hand:\n {hand}";
        }
    }

    // -------------------------------------------------------------------------------------------

    public class Hand
    {
        private Card[] cards;

        public Hand()
        {
            cards = new Card[0];                        // Empty hand
        }

        public int Size()
        {
            return cards.Length;
        }

        public Card[] GetCards()
        {
            Card[] myCards = new Card[Size()];          // Return a copy
            Array.Copy(cards, myCards, Size());         //   so that our cards cannot be changed
            return myCards;
        }

        public void AddCard(Card card)
        {
            Array.Resize(ref cards, Size() + 1);
            cards[Size() - 1] = card;
        }

        public Card RemoveRandomCard()
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

            return s;
        }
    }

}