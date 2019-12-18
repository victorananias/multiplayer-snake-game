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

            while (!stoppingToken.IsCancellationRequested)
            {
                _gameStateService.State.Snakes.ForEach(snake => snake.Update());

                var collidables = new List<ICollidable>();

                _gameStateService.State.Snakes.ForEach(snake => {
                    collidables.Add(snake);
                });

                _gameStateService.State.Fruits.ForEach(fruit => {
                    collidables.Add(fruit);
                });

                collidables.ForEach(collidable1 =>
                {

                    collidables.ForEach(collidable2 =>
                    {
                        if (collidable1 == collidable2)
                        {
                            return;
                        }

                        collidable1.Hitboxes.ForEach(hitbox =>
                        {

                        });
                    });
                });

                await clients.All.SendAsync("UpdateGameState", _gameStateService.State);

                await Task.Delay(100, stoppingToken);
            }
        }
    }
}
