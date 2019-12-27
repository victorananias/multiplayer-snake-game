using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeGameBackend.Services;

namespace SnakeGameBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameStateController : ControllerBase
    {
        private GameStateService _gameStateService;

        public GameStateController(

            GameStateService gameStateService
        )
        {
            _gameStateService = gameStateService;
        }


        [HttpPost("clear")]
        public IActionResult ClearGameState()
        {
            _gameStateService.ResetState();
            return Ok();
        }
    }
}