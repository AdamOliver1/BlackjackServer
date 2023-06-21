using BlackjackServer.Models;

namespace BlackjackServer.Controllers.Responses
{
    public class EndGameRes
    {
        public EndGameRes(List<Card> dealerCards, string winnerId)
        {
            DealerHand = dealerCards;
            WinnerId = winnerId;
        }

        public List<Card> DealerHand { get; set; }
        public string WinnerId { get; set; }
    }
}
