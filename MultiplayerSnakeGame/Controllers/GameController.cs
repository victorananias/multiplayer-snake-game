using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MultiplayerSnakeGame.ViewModels;

namespace MultiplayerSnakeGame.Controllers
{
    public class GameController : Controller
    {
        [HttpGet("")]
        public IActionResult IndexWithoutGameId()
        {
            var gameId = Guid.NewGuid().ToString();
            return Redirect($"/{gameId}");
        }

        [HttpGet("{gameId}")]
        public IActionResult Index(string gameId)
        {
            return View(new GameInfo
            {
                Id = gameId
            });
        }
    }
}