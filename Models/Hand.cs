using System.Text.Json.Serialization;

namespace BlackjackServer.Models
{
    public class Hand
    {
        public string PlayerId { get; set; }
        public List<Card> Cards { get; set; }

        public Hand(List<Card> cards)
        {
            PlayerId = Guid.NewGuid().ToString();
            Cards = cards;
        }

        public int getSum()
        {
            return Cards.Sum(c => c.Number);
        }
    }
}
