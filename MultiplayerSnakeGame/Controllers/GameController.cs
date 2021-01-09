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
        [HttpGet]
        public IActionResult Index(string game)
        {
            if (string.IsNullOrWhiteSpace(game))
            {
                return RedirectToAction("Index", routeValues: new { game = Guid.NewGuid().ToString() });
            }

            return View();
        }
    }
}