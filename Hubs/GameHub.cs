using BlackjackServer.Hnadlers.API;
using BlackjackServer.Hubs.DTOs;
using BlackjackServer.Hubs.DTOs.Responses;
using BlackjackServer.Models;
using BlackjackServer.Services;
using BlackjackServer.Services.API;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace BlackjackServer.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGamesManager _gamesManager;

        public GameHub(IGamesManager gamesManager)
        {
            _gamesManager = gamesManager;
        }
        public async Task JoinGame(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            IGameHandler game = _gamesManager.GetGameById(gameId);
            game.AddPlayer();
            await Clients.Group(gameId).SendAsync("NotifynewPlayer", new GameDTO(game));
        }

        public async Task ReciveStartGame(string gameId)
        {
            IGameHandler game = _gamesManager.GetGameById(gameId);
            await Clients.Group(gameId).SendAsync("NotifyStartGame", new GameDTO(game.StartGame()));
        }
        public async Task ReciveCardDrawn(string gameId, string playerId)
        {
            await Clients.Group(gameId).SendAsync("NotifyCardDrawn", new GameDTO(_gamesManager.GetGameById(gameId).PlayTurn(playerId)));
        }

        public async Task ReciveNextTurn(string gameId)
        {
            await Clients.Group(gameId).SendAsync("NotifyNextTurn", new GameDTO(_gamesManager.GetGameById(gameId).NextTurn()));
        }
        public async Task ReciveFinishRound(string gameId)
        {
            List<string> winnersIds = _gamesManager.GetGameById(gameId).FinishRound();
            GameResultsDTO gameResultsDTO = new GameResultsDTO(_gamesManager.GetGameById(gameId), winnersIds);
            await Clients.Group(gameId).SendAsync("NotifyFinishRound", gameResultsDTO);
        }
        public async Task RecivePlayerExitGame(string gameId, string playerId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);
            await Clients.Group(gameId).SendAsync("NotifyPlayerExitGame", new GameDTO(_gamesManager.GetGameById(gameId).PlayerExitGame(playerId)));
        }
        public async Task RecivePlayAnotherRound(string gameId)
        {
            await Clients.Group(gameId).SendAsync("NotifyNewRound", new GameDTO(_gamesManager.GetGameById(gameId).PlayAnotherRound()));
        }
    }
}

