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
        private GameStateService _gameStateService;

        public GameHub(GameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }

        public async override Task OnConnectedAsync()
        {
            _gameStateService.AddSnake(Context.ConnectionId);

            await Clients.All.SendAsync("ReceiveMessage", _gameStateService.State);
            //await Clients.Client(this.Context.ConnectionId).SendAsync("Ok", Clients.All);
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            _gameStateService.RemoveSnake(Context.ConnectionId);
        }
    }
}
