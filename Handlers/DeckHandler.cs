using BlackjackServer.Hnadlers;
using BlackjackServer.Hnadlers.API;
using BlackjackServer.Models;

namespace BlackjackServer.Services
{
    public class DeckHandler 
    {
        private List<CardHandler> cards;

        public DeckHandler()
        {
            _createDeck();
        }
     


        public CardHandler DrawCard()
        {
            if (cards.Count == 0)
                throw new Exception("The deck is empty");

            CardHandler topCard = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return topCard;
        }

        public List<CardHandler> DrawHand()
        {
            if (cards.Count == 0)
                throw new Exception("The deck is empty");
            return new List<CardHandler> { DrawCard(), DrawCard() };
        }

        private void _createDeck()
        {
            cards = new List<CardHandler>();

            foreach (var suit in Enum.GetValues(typeof(Suit)))
            {
                for (short i = 1; i <= 13; i++)
                {
                    this.cards.Add(new CardHandler(i, (Suit)suit));
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
                CardHandler value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

     
    }
}
