using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MultiplayerSnakeGame.Data;
using MultiplayerSnakeGame.Entities;
using MultiplayerSnakeGame.Hubs;

namespace MultiplayerSnakeGame.Services
{
    public class GamesService
    {
        private IHubContext<GameHub> _hub;
        private GamesContext _context;

        public GamesService(
            GamesContext context,
            IHubContext<GameHub> hub
        )
        {
            _context = context;
            _hub = hub;
        }

        public void AddPlayer(string gameId, string playerId)
        {
            if (gameId == null)
            {
                gameId = Guid.NewGuid().ToString();
            }

            var game = GetGameById(gameId);

            if (game == null)
            {
                game = new Game(gameId);
                _context.Games.Add(game);
            }

            var snake = game.CreateSnake(playerId);
            game.GenerateFruit();
            _context.Snakes.Add(snake);

            _hub.Groups.AddToGroupAsync(playerId, groupName: gameId);
        }

        public async Task RunGames()
        {
            foreach (var game in _context.Games)
            {
                game.Run();
                await _hub.Clients.Groups(game.Id).SendAsync("Update", game);
            }
        }

        public void RemoveSnake(string snakeId)
        {
            var playerInfo = _context.Snakes.SingleOrDefault(p => p.Id == snakeId);
            var game = _context.Games.SingleOrDefault(g => g.Id == playerInfo.GameId);
                
            game.RemoveSnakeById(snakeId);

            if (!game.Snakes.Any())
            {
                _context.Games.Remove(game);
            }

            Console.WriteLine($"There are {_context.Games.Count} games being played.");
        }

        public void MoveOrBoost(string snakeId, string direction)
        {
            var player = _context.Snakes.FirstOrDefault(p => p.Id == snakeId);
            GetGameById(player.GameId)?.MoveSnake(snakeId, direction);
        }

        public void StopSnakeBoost(string playerId)
        {
            var player = _context.Snakes.FirstOrDefault(p => p.Id == playerId);
            GetGameById(player.GameId)?.ReduceSnakeSpeed(playerId);
        }

        private Game GetGameById(string gameId)
        {
            return _context.Games.FirstOrDefault(g => g.Id == gameId);
        }
    }
}