using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SnakeGameBackend.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SnakeGameBackend.Entities;

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
                var colliadablesChecked = new Dictionary<string, List<string>>();
                _gameStateService.State.Snakes.ForEach(snake => snake.Update());

                var collidables = new List<ICollidable>();

                if (_gameStateService.State.Snakes.Any())
                {

                    _gameStateService.State.Snakes.ForEach(snake => collidables.Add(snake));
                    _gameStateService.State.Fruits.ForEach(fruit => collidables.Add(fruit));

                    foreach (var collidable1 in collidables)
                    {
                        foreach (var collidable2 in collidables)
                        {
                            if (collidable1.Id == collidable2.Id)
                            {
                                continue;
                            }

                            if (!colliadablesChecked.ContainsKey(collidable1.Id))
                            {
                                colliadablesChecked[collidable1.Id] = new List<string>();
                            }

                            if (!colliadablesChecked.ContainsKey(collidable2.Id))
                            {
                                colliadablesChecked[collidable2.Id] = new List<string>();
                            }

                            if (
                                colliadablesChecked[collidable1.Id].Contains(collidable2.Id)
                                || colliadablesChecked[collidable2.Id].Contains(collidable1.Id)
                            )
                            {
                                continue;
                            }

                            var collided = from hitbox1 in collidable1.Hitboxes
                                           from hitbox2 in collidable2.Hitboxes
                                           where (
                                               hitbox1.X >= hitbox2.X
                                               && hitbox1.X + hitbox1.Width <= hitbox2.X + hitbox2.Width
                                               && hitbox1.Y >= hitbox2.Y
                                               && hitbox1.Y + hitbox1.Height <= hitbox2.Y + hitbox1.Height
                                           )
                                           select hitbox1;

                            if (collided.Any())
                            {
                                collidable1.CollidedTo(collidable2);
                                collidable1.CollidedTo(collidable1);

                                if (collidable1.GetType() == typeof(Snake))
                                {
                                    ((Snake)collidable1).Grow();

                                    _gameStateService.RemoveFruit(collidable2.Id);
                                    _gameStateService.GenerateFruit();
                                }

                                if (collidable2.GetType() == typeof(Snake))
                                {
                                    ((Snake)collidable2).Grow();

                                    _gameStateService.RemoveFruit(collidable1.Id);
                                    _gameStateService.GenerateFruit();
                                }
                            }

                            colliadablesChecked[collidable1.Id].Add(collidable2.Id);
                            colliadablesChecked[collidable2.Id].Add(collidable1.Id);
                        }
                    }
                }

                await clients.All.SendAsync("UpdateGameState", _gameStateService.State, cancellationToken: stoppingToken);

                await Task.Delay(100, stoppingToken);
            }
        }
    }
}
