using BlackjackServer.Services.API;
using System.Collections.Concurrent;

namespace BlackjackServer.Services
{
    public class GamesManager : IGamesManager
    {
        //thread-safe Dictionary
        private ConcurrentDictionary<string, GameHandler> _games { get; set; } = new ConcurrentDictionary<string, GameHandler>();

        public GameHandler GetGame(string gameId)
        {
            if (!_games.TryGetValue(gameId, out GameHandler? game))
                throw new InvalidOperationException("Game ID is nor valid");

            return game;
        }

        public GameHandler StartNewGame(short numOfPlayers)
        {
            GameHandler game = new GameHandler(numOfPlayers);
            _games[game.GameId] = game;
            return game;
        }

    
    }
}
