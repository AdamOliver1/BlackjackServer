using System.Text.Json.Serialization;

namespace BlackjackServer.Hnadlers
{
    public class PlayerHandler
    {
        public string PlayerId { get; private set; }
        public List<CardHandler> Cards { get; set; }
        public PlayerHandler? NextPlayer { get; set; }

        public bool IsHandValid => getSumOfHandValue() <= 21;

        public PlayerHandler(List<CardHandler> cards)
        {
            PlayerId = Guid.NewGuid().ToString();
            Cards = cards;
        }



        public int getSumOfHandValue()
        {
            int aces = Cards.Count(card => card.Number == 1);
            int sum = Cards.Where(card => card.Number != 1).Sum(card => card.GetCardValue());

            for (int i = 0; i < aces; i++)
            {
                if (sum + 11 <= 21)
                    sum += 11;
                else
                    sum += 1;
            }

            return sum;
        }
    }



}
