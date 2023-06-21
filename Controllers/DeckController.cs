using BlackjackServer.Controllers.Responses;
using BlackjackServer.Models;
using BlackjackServer.Models.RequestsBody;
using BlackjackServer.Services;
using BlackjackServer.Services.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackjackServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        private readonly IGamesManager _gamesManager;

        public DeckController(IGamesManager gamesManager)
        {
            _gamesManager = gamesManager;
        }

        [HttpPost("playTurn")]
        public ActionResult<PlayTurnRes> PlayTurn([FromBody] PlayTurnReq playTurnReq)
        {
            List<Card> playerHand = playTurnReq.Hand;
            string gameId = playTurnReq.GameId;

            bool isOk = _gamesManager.GetGame(gameId).PlayTurn(playerHand);
            return Ok(new PlayTurnRes(playerHand, isOk));
        }

      
    }
}
