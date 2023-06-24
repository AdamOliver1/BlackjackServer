using BlackjackServer.Hnadlers.API;
using BlackjackServer.Hubs.DTOs;
using BlackjackServer.Models;

namespace BlackjackServer.Services.API
{
    public interface IGamesManager
    {
        IGameHandler GetGameById(string gameId);
        //GameHandler AddPlayerToGame(string gameId);
        IGameHandler NewGame();

        string[] GetAllGames();
    }
}
