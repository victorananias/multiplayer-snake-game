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

        public void Move(string gameId, string direction)
        {
            _gamesService.MoveSnake(gameId, Context.ConnectionId, direction);
        }

        public void ReduceSnakeSpeed(string gameId)
        {
            _gamesService.ReduceSnakeSpeed(gameId, Context.ConnectionId);
        }

        public void JoinGame(string gameId = null)
        {
            Console.WriteLine($"Player {Context.ConnectionId} joined game \"{gameId}\"");
            _gamesService.JoinGame(gameId, Context.ConnectionId);
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
