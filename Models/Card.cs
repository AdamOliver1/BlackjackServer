using BlackjackServer.Exceptions;

namespace BlackjackServer.Models
{
    public class Card
    {
        public short Number { get; set; }
        public Suit Suit { get; set; }


        public Card(short number, Suit suit)
        {
            if(number < 0 || number > 13) throw new InvalidCardNumberException(number);
            Number = number;
            Suit = suit;
        }
    }
}
