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
            Console.WriteLine("started");
            var clients = _hubContext.Clients;

            while (!stoppingToken.IsCancellationRequested)
            {
                var colliadablesChecked = new Dictionary<string, List<string>>();
                _gameStateService.State.Snakes.ForEach(snake => snake.Update());

                var collidables = new List<ICollidable>();

                _gameStateService.State.Snakes.ForEach(snake => collidables.Add(snake));
                _gameStateService.State.Fruits.ForEach(fruit => collidables.Add(fruit));

                collidables.ForEach(collidable1 =>
                {
                    collidables.ForEach(collidable2 =>
                    {

                        if (collidable1.Id == collidable2.Id)
                        {
                            return;
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
                            return;
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
                            Console.WriteLine("collided");
                        } 

                        colliadablesChecked[collidable1.Id].Add(collidable2.Id);
                        colliadablesChecked[collidable2.Id].Add(collidable1.Id);
                    });
                });

                await clients.All.SendAsync("UpdateGameState", _gameStateService.State, cancellationToken: stoppingToken);

                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
