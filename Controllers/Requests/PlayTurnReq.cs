namespace BlackjackServer.Models.RequestsBody
{
    public class PlayTurnReq
    {
        public string GameId { get; set; }
        public List<Card> Hand { get; set; }

    }
}
