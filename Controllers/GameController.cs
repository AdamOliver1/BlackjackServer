using BlackjackServer.Models;
using BlackjackServer.Services;
using BlackjackServer.Services.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlackjackServer.Hubs;
using Microsoft.AspNetCore.SignalR;
using BlackjackServer.Hubs.DTOs;

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
         
            //_gameHub = gameHub;
        }

        [HttpGet("games")]
        public  ActionResult<string[]> getAllGames()
        {
            return Ok(_gamesManager.GetAllGames());
        }

        [HttpGet("available")]
        public ActionResult<string[]> getIsRoomAvailable(string gameId)
        {
            return Ok(_gamesManager.GetGameById(gameId).CheckIfAvilableToAddPlayer());
        }

    }
}
