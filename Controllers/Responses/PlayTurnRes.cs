using BlackjackServer.Models;
using System.Text.Json.Serialization;

namespace BlackjackServer.Controllers.Responses
{
    public class PlayTurnRes
    {
        public PlayTurnRes(List<Card> hand, bool isOk)
        {
            this.Hand = hand;
            this.IsOk = isOk;
        }
        [JsonInclude]
        public bool IsOk { get; set; }
        [JsonInclude]
        public List<Card> Hand { get; set; }
    }
}
