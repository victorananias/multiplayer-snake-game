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
            
            _gamesService.AddSnake(gameId, Context.ConnectionId);
            
            Groups.AddToGroupAsync(Context.ConnectionId, groupName: gameId);
        }

        public void KeyPressed(string key)
        {
            var direction = "";

            switch (key)
            {
                case "d":
                    direction = "right";
                    break;
                case "a":
                    direction = "left";
                    break;
            
                case "w":
                    direction = "up";
                    break;
                case "s":
                    direction = "down";
                    break;
            }

            if (string.IsNullOrEmpty(direction)) 
            {
                return;
            }

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
            _gamesService.DisconnectPlayer(Context.ConnectionId);
            Console.WriteLine($"Id {Context.ConnectionId} connected.");
        }
    }
}
