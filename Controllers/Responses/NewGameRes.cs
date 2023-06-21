using BlackjackServer.Models;
using BlackjackServer.Services;
using System.Text.Json.Serialization;

namespace BlackjackServer.Controllers.Responses
{
    public class NewGameRes
    {

        public string GameId { get; set; }

        [JsonPropertyName("playersHands")]
        public List<Hand> _playersHands { get; set; }

        [JsonPropertyName("dealerCard")]
        public Card _dealerCard { get; set; }

        [JsonIgnore]
        GameHandler _game;

        public NewGameRes(GameHandler game)
        {
            _game = game;
            GameId = _game.GameId;
            _playersHands = new List<Hand>();
            _dealerCard = _game.Deck.DrawCard();
            _createPlayersHands();
        }


        private void _createPlayersHands()
        {
            for (short i = 0; i < _game.NumOfPlayers; i++)
                _playersHands.Add(new Hand(_game.Deck.DrawHand()));
        }
    }
}
