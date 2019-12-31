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

        public void Move(string direction)
        {
            _gamesService.MoveSnake(Context.ConnectionId, direction);
        }

        public void ReduceSnakeSpeed()
        {
            _gamesService.ReduceSnakeSpeed(Context.ConnectionId);
        }

        public void JoinGame(string gameId = null)
        {
            Console.WriteLine($"Player {Context.ConnectionId} joined game \"{gameId}\"");
            _gamesService.AddPlayer(gameId, Context.ConnectionId);
        }

        public async override Task OnConnectedAsync()
        {
            Console.WriteLine($"Id {Context.ConnectionId} connected.");
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            _gamesService.RemovePlayer(Context.ConnectionId);
            Console.WriteLine($"Id {Context.ConnectionId} connected.");
        }
    }
}
