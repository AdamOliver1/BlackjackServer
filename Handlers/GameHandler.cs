using BlackjackServer.Hnadlers;
using BlackjackServer.Hnadlers.API;
using BlackjackServer.Hubs.DTOs;
using System.Collections.Concurrent;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace BlackjackServer.Services
{
    public class GameHandler : IGameHandler
    {
        public string GameId { get; }
        public DeckHandler Deck { get;private set; }
        public List<PlayerHandler> Players { get;private set; }
        public PlayerHandler Dealer { get;private set; }
        public PlayerHandler? CurrentPlayer { get; private set; }

        public bool IsGameActive { get;private set; }
        public GameHandler()
        {
            Players = new List<PlayerHandler>();
            Deck = new DeckHandler();
            Dealer = new PlayerHandler(new List<CardHandler> { Deck.DrawCard() });
            GameId = Guid.NewGuid().ToString();
        }


        public IGameHandler PlayTurn(string playerId)
        {
            PlayerHandler? player = Players.FirstOrDefault(player => playerId == player.PlayerId);

            if (player == null)
                throw new ArgumentException("Player doesn't exist");

            player.Cards.Add(Deck.DrawCard());
            return this;
        }


        public List<string> FinishRound()
        {
            IsGameActive = false;
            List<string> WinningPlayersId = new List<string>();
            drawDealerCards();
            calcWinningPlayersByDealerScore(WinningPlayersId);


            return WinningPlayersId;
        }

        public bool CheckIfAvilableToAddPlayer()
        {
            if (IsGameActive) return false;
            return Players.Count < 3;
        }

        public PlayerHandler AddPlayer()
        {
            if (Players.Count == 3)
                throw new InvalidOperationException("Room is full");
            PlayerHandler newPlayer = new PlayerHandler(Deck.DrawHand());
            if (Players.Count == 0) CurrentPlayer = newPlayer;
            Players.Add(newPlayer);
            return newPlayer;
        }


        public IGameHandler StartGame()
        {
            IsGameActive = true;
            for (int i = 0; i < Players.Count - 1; i++)
                Players[i].NextPlayer = Players[i + 1];

            return (this);
        }


        public IGameHandler NextTurn()
        {
            if (CurrentPlayer == null)
                throw new InvalidOperationException("This is the last player");

            CurrentPlayer = CurrentPlayer.NextPlayer;
          
            return this;
        }

        public IGameHandler PlayAnotherRound()
        {
            Deck = new DeckHandler();
            Dealer = new PlayerHandler(new List<CardHandler> { Deck.DrawCard() });

            foreach (PlayerHandler player in Players)
                player.Cards = Deck.DrawHand();

            CurrentPlayer = Players[0];

            return StartGame();
        }

        public IGameHandler PlayerExitGame(string playerId)
        {
            PlayerHandler? playerToRemove = Players.FirstOrDefault(p => p.PlayerId == playerId);
            if (playerToRemove == null) throw new InvalidOperationException("Player doesn't exist");
           
            PlayerHandler? previousPlayer = Players.FirstOrDefault(p => p.NextPlayer?.PlayerId == playerId);

            if (previousPlayer != null)
                previousPlayer.NextPlayer = playerToRemove.NextPlayer;

            Players.Remove(playerToRemove);
            if(Players.Count == 0)
            {
                Deck = new DeckHandler();
                Dealer = new PlayerHandler(new List<CardHandler> { Deck.DrawCard() });
            }
            return this;
        }


        void calcWinningPlayersByDealerScore(List<string> WinningPlayersId)
        {
            int dealerHandSum = Dealer.getSumOfHandValue();
            foreach (PlayerHandler player in Players)
            {
                int playerHandSum = player.getSumOfHandValue();
                if (playerHandSum > 21) continue;
                if(dealerHandSum > 21 || playerHandSum >= dealerHandSum)
                    WinningPlayersId.Add(player.PlayerId);
            }
        }


         void drawDealerCards()
        {
            int dealerHandSum = Dealer.getSumOfHandValue();
            while (dealerHandSum < 17)
            {
                CardHandler newCard = Deck.DrawCard();
                Dealer.Cards.Add(newCard);
                if (newCard.Number == 1)
                {
                    if (dealerHandSum + 11 <= 21) dealerHandSum += 11;
                    else dealerHandSum += 1;
                }
                else
                    dealerHandSum += newCard.GetCardValue();
            }
        }

     
    }
}
