using BlackjackServer.Exceptions;
using BlackjackServer.Models;

namespace BlackjackServer.Hnadlers
{
    public class CardHandler
    {
        public int Number { get; set; }
        public Suit Suit { get; set; }


        public CardHandler(short number, Suit suit)
        {
            if (number < 1 || number > 13) throw new InvalidCardNumberException(number);
            Number = number;
            Suit = suit;
        }


        public int GetCardValue()
        {
            return Number > 10 ? 10 : Number;
        }
    }
}
