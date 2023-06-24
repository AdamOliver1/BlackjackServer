using BlackjackServer.Hnadlers.API;
using BlackjackServer.Hubs;
using BlackjackServer.Hubs.DTOs;
using BlackjackServer.Models;
using BlackjackServer.Services.API;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace BlackjackServer.Services
{
    public class GamesManager : IGamesManager
    {

        private ConcurrentDictionary<string, IGameHandler> _games { get; set; }
        public GamesManager()
        {
            _games = new ConcurrentDictionary<string, IGameHandler>();

            NewGame();
            NewGame();
            NewGame();
        }

        public IGameHandler GetGameById(string gameId)
        {
            if (!_games.TryGetValue(gameId, out IGameHandler? game))
                throw new InvalidOperationException("Game ID is nor valid");

            return game;
        }

        public string[] GetAllGames()
        {
            return _games.Keys.ToArray();
        }

        public IGameHandler NewGame()
        {

            IGameHandler newGame = new GameHandler();
            _games[newGame.GameId] = newGame;
            return newGame;
        }

    }
}
