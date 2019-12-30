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
        private IHubContext<GameHub> _hub;
        private GamesService _gameService;

        public GameHostedService(
            IHubContext<GameHub> hub, 
            GamesService gameService
        )
        {
            _hub = hub;
            _gameService = gameService;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var clients = _hub.Clients;

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var game in _gameService.Games)
                {
                    game.Run();
                    await clients.Groups(game.Id).SendAsync("Update", game, cancellationToken: stoppingToken);
                }

                await Task.Delay(10, stoppingToken);
            }
        }
    }
}
