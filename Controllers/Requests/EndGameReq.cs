using BlackjackServer.Models;

namespace BlackjackServer.Controllers.Requests
{
    public class EndGameReq
    {
        public string GameId { get; set; }
        public List<Hand> playersHands { get; set; }
        public Card DealerCard { get; set; }

    }
}
