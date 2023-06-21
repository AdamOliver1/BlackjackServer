using BlackjackServer.Controllers.Requests;
using BlackjackServer.Controllers.Responses;
using BlackjackServer.Models;
using System.Collections.Concurrent;
using System.Reflection.Metadata;

namespace BlackjackServer.Services
{
    public class GameHandler
    {
        public string GameId { get; }
        public short NumOfPlayers { get; set; }
        public DeckHandler Deck { get; }

        public GameHandler(short numOfPlayers = 1)
        {
            if (numOfPlayers <= 0 || numOfPlayers >= 4)
                throw new ArgumentOutOfRangeException("Number of players must be above 0 and under 4");

            GameId = Guid.NewGuid().ToString();
            Deck = new DeckHandler();
            NumOfPlayers = numOfPlayers;
        }

        public bool PlayTurn(List<Card> cards)
        {
            if (cards.Sum(c => c.Number) >= 21)
                throw new ArgumentException("You can not draw another card if you have 21 or above");

            cards.Add(Deck.DrawCard());
            return cards.Sum(c => c.Number) <= 21;
        }

        public EndGameRes EndGame(EndGameReq endGameReq)
        {
            List<Card> dealerHand = new List<Card> { endGameReq.DealerCard };
            int dealerHandSum = _drawDealerCards(dealerHand);

            int highestPlayerSum = 0;
            string WinningPlayerId = string.Empty;

            _calcBestHand(endGameReq.playersHands, ref highestPlayerSum, ref WinningPlayerId);

            return new EndGameRes(dealerHand, highestPlayerSum > dealerHandSum ? WinningPlayerId : "");

        }

        private void _calcBestHand(List<Hand> playersHands, ref int highestSum, ref string WinningPlayerId)
        {
            foreach (var hand in playersHands)
            {
                int sum = hand.getSum();
                if (sum > highestSum && sum <= 21)
                {
                    highestSum = sum;
                    WinningPlayerId = hand.PlayerId;
                }
            }
        }

        private int _drawDealerCards(List<Card> dealerHand)
        {
            //TODO CATCH ERROR
            short dealerHandSum = dealerHand[0].Number;
            while (dealerHandSum < 17)
            {
                Card newCard = Deck.DrawCard();
                dealerHand.Add(newCard);
                dealerHandSum += newCard.Number;
            }

            return dealerHandSum > 21 ? 0 : dealerHandSum;
        }
    }
}
