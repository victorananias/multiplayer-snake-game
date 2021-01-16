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
        private GamesService _gamesService;
        private const int _delayTime = 10;

        public GamesHostedService(
            GamesService gamesService
        )
        {
            _gamesService = gamesService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _gamesService.ExecuteAsync();

                await DelayTaskAsync(stoppingToken);
            }
        }

        private async Task DelayTaskAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(_delayTime, stoppingToken);
        }
    }
}
