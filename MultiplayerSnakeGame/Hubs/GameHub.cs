using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MultiplayerSnakeGame.Services;

namespace MultiplayerSnakeGame.Hubs
{
    public class GameHub : Hub
    {
        private GamesService _gamesService;

        public GameHub(GamesService gamesService)
        {
            _gamesService = gamesService;
        }

        public void JoinGame(string gameId = null)
        {
            Console.WriteLine($"Player {Context.ConnectionId} joined game \"{gameId}\"");
            _gamesService.AddPlayer(gameId, Context.ConnectionId);
        }

        public void KeyPressed(string key)
        {
            var direction = "";

            if (key == "d")
            {
                direction = "right";
            }
            else if (key == "a")
            {
                direction = "left";
            }
            else if (key == "w")
            {
                direction = "up";
            }
            else if (key == "s")
            {
                direction = "down";
            }
            else if (key == " ")
            {
                direction = "";
            }

            // if (string.IsNullOrEmpty(direction)) 
            // {
            //     return;
            // }

            _gamesService.MoveOrBoost(Context.ConnectionId, direction);
        }

        public void KeyReleased(string key)
        {
            _gamesService.StopSnakeBoost(Context.ConnectionId);
        }

        public async override Task OnConnectedAsync()
        {
            Console.WriteLine($"Id {Context.ConnectionId} connected.");
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            _gamesService.RemoveSnake(Context.ConnectionId);
            Console.WriteLine($"Id {Context.ConnectionId} connected.");
        }
    }
}
