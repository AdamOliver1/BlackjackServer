using BlackjackServer.Controllers.Responses;
using BlackjackServer.Models.RequestsBody;
using BlackjackServer.Models;
using BlackjackServer.Services;
using BlackjackServer.Services.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackjackServer.Controllers.Requests;

namespace BlackjackServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
       

        private readonly IGamesManager _gamesManager;

        public GameController(IGamesManager gamesManager)
        {
            _gamesManager = gamesManager;
        }

        [HttpPost("newGame")]
        public ActionResult<NewGameRes> NewGame([FromQuery] short numOfPlayers)
        {
            if (numOfPlayers <= 0 || numOfPlayers >= 4)
                return BadRequest("Number of players must be above 0");

            GameHandler game = _gamesManager.StartNewGame(numOfPlayers);
            return Ok(new NewGameRes(game)); 
        }

        [HttpPost("endgame")]
        public ActionResult<EndGameRes> EndGame([FromBody] EndGameReq endGameReq)
        {
            return Ok(_gamesManager.GetGame(endGameReq.GameId).EndGame(endGameReq));
        }
    }
}
