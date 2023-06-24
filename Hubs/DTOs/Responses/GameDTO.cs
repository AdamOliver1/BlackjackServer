using BlackjackServer.Hnadlers;
using BlackjackServer.Hnadlers.API;
using BlackjackServer.Services;
using System.Text.Json.Serialization;

namespace BlackjackServer.Hubs.DTOs
{
    public class GameDTO
    {
        

        public string GameId { get; set; }

        [JsonPropertyName("players")]
        public List<PlayerHandler> players { get; set; }

        [JsonPropertyName("dealer")]
        public PlayerHandler Dealer { get; set; }

        public PlayerHandler? CurrentPlayer { get; set; }

        [JsonIgnore]
        IGameHandler _game;

        public GameDTO(IGameHandler game)
        {
            CurrentPlayer = game.CurrentPlayer;
            _game = game;
            GameId = _game.GameId;
            players = _game.Players;
            Dealer = _game.Dealer;

           
        }

    }
}
