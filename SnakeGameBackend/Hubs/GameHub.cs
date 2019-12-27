using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SnakeGameBackend.Services;

namespace SnakeGameBackend.Hubs
{
    public class GameHub : Hub
    {
        private GameStateService _gameService;

        public GameHub(GameStateService gameService)
        {
            _gameService = gameService;
        }

        public void Move(string direction)
        {
            _gameService.MoveSnake(Context.ConnectionId, direction);
        }

        public void ReduceSnakeSpeed()
        {
            _gameService.ReduceSnakeSpeed(Context.ConnectionId);
        }

        public async override Task OnConnectedAsync()
        {
            _gameService.AddSnake(Context.ConnectionId);
            _gameService.GenerateFruit();

            await Clients.All.SendAsync("ReceiveMessage", _gameService.State);
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            _gameService.RemoveSnake(Context.ConnectionId);
        }
    }
}
