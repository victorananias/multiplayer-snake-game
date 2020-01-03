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

        public void AddSnake(string gameId, string snakeId)
        {
            if (string.IsNullOrWhiteSpace(gameId))
            {
                return;
            }

            var game = GetGameById(gameId);

            if (game == null)
            {
                game = new Game(gameId);
                _context.Games.Add(game);
            }

            var snake = game.CreateSnake(snakeId);
            
            if (snake == null)
            {
                return;
            }

            game.GenerateFruit();
            _context.Snakes.Add(snake);
        }

        public async Task RunGames()
        {
            foreach (var game in _context.Games)
            {
                game.Run();
                await _hub.Clients.Groups(game.Id).SendAsync("Update", game);
            }
        }

        public void KillSnakeById(string snakeId)
        {
            var snake = _context.GetSnakeById(snakeId);
            snake.Die();
        }

        public void MoveOrBoost(string snakeId, string direction)
        {
            var player = _context.GetSnakeById(snakeId);

            if (player == null) 
            {
                return;
            }

            GetGameById(player.GameId)?.MoveSnake(snakeId, direction);
        }

        public void StopSnakeBoost(string snakeId)
        {
            var player = _context.GetSnakeById(snakeId);

            if (player == null) 
            {
                return;
            }

            GetGameById(player.GameId)?.ReduceSnakeSpeed(snakeId);
        }

        private Game GetGameById(string gameId)
        {
            return _context.Games.FirstOrDefault(g => g.Id == gameId);
        }
    }
}