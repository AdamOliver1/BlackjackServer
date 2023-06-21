using BlackjackServer.Models;

namespace BlackjackServer.Services
{
    public class DeckHandler
    {
        private List<Card> cards;

        public DeckHandler()
        {
            _createDeck();
        }

       

        public Card DrawCard()
        {
            if (cards.Count == 0)
            {
                throw new Exception("The deck is empty");
            }

            Card topCard = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return topCard;
        }

        public List<Card> DrawHand()
        {
            if (cards.Count == 0)
            {
                throw new Exception("The deck is empty");
            }

            return new List<Card> { DrawCard(), DrawCard() };

        }

        private void _createDeck()
        {
            this.cards = new List<Card>();

            foreach (var suit in Enum.GetValues(typeof(Suit)))
            {
                for (short i = 1; i <= 13; i++)
                {
                    this.cards.Add(new Card(i, (Suit)suit));
                }
            }

            _shuffle();
        }

        public void _shuffle()
        {
            Random rng = new Random();

            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }
    }
}
