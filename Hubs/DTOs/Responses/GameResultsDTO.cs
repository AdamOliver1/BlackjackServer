using BlackjackServer.Hnadlers.API;
using BlackjackServer.Models;
using BlackjackServer.Services;

namespace BlackjackServer.Hubs.DTOs
{
    public class GameResultsDTO
    {
        public GameResultsDTO(IGameHandler game, List<string> winnersId)
        {
            Game = new GameDTO(game);
            WinnersId = winnersId;
        }

        public GameDTO Game { get; set; }
        public List<string>  WinnersId { get; set; }
    }
}
