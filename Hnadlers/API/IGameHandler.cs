using BlackjackServer.Services;

namespace BlackjackServer.Hnadlers.API
{
    public interface IGameHandler
    {
        string GameId { get; }
        DeckHandler Deck { get; }
        List<PlayerHandler> Players { get; }
        PlayerHandler Dealer { get; }
        PlayerHandler? CurrentPlayer { get; }
        bool IsGameActive { get; }

        IGameHandler PlayTurn(string playerId);
        List<string> FinishRound();
        bool CheckIfAvilableToAddPlayer();
        PlayerHandler AddPlayer();
        IGameHandler StartGame();
        IGameHandler NextTurn();
        IGameHandler PlayAnotherRound();
        IGameHandler PlayerExitGame(string playerId);
    }
}
