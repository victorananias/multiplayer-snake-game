using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SnakeGameBackend.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGameBackend.Services
{
    public class GameHostedService : BackgroundService
    {
        private IHubContext<GameHub> _hubContext;
        private GameStateService _gameStateService;

        public GameHostedService(IHubContext<GameHub> hubContext, GameStateService gameStateService)
        {
            _hubContext = hubContext;
            _gameStateService = gameStateService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var clients = _hubContext.Clients;

            if (this._gameStateService.State.Snakes.Count > 0)
            {
                this._gameStateService.State.Snakes[0].Head.X += 20;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await clients.All.SendAsync("UpdateGameState", this._gameStateService.State);

                await Task.Delay(1000);
            }
        }
    }
}
