using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MultiplayerSnakeGame.Hubs;
using MultiplayerSnakeGame.Entities;

namespace MultiplayerSnakeGame.Services
{
    public class GameHostedService : BackgroundService
    {
        private IHubContext<GameHub> _hubContext;
        private GameService _game;

        public GameHostedService(
            IHubContext<GameHub> hubContext, 
            GameService game
        )
        {
            _hubContext = hubContext;
            _game = game;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var clients = _hubContext.Clients;

            while (!stoppingToken.IsCancellationRequested)
            {
                _game.Update();

                await clients.All.SendAsync("UpdateView", _game.State, cancellationToken: stoppingToken);

                await Task.Delay(10, stoppingToken);
            }
        }
    }
}
