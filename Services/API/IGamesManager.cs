namespace BlackjackServer.Services.API
{
    public interface IGamesManager
    {
         GameHandler StartNewGame(short numOfPlayers);
        GameHandler GetGame(string gameId);
    }
}
