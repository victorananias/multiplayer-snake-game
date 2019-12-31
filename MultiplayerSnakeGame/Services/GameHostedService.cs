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
        private GamesService _gameService;

        public GameHostedService(
            GamesService gameService
        )
        {
            _gameService = gameService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _gameService.RunGames();

                await Task.Delay(10, stoppingToken);
            }
        }
    }
}
