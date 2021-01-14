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
    public class GamesHostedService : BackgroundService
    {
        private GamesService _gameService;

        public GamesHostedService(
            GamesService gameService
        )
        {
            _gameService = gameService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _gameService.RunAsync();

                await Task.Delay(10, stoppingToken);
            }
        }
    }
}
