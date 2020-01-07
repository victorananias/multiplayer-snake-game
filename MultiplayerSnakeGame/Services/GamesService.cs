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
            var game = GetGameById(gameId);

            if (game == null)
            {
                game = new Game(gameId);
                _context.AddGame(game);
            }

            var snake = game.CreateSnake(snakeId);
            
            if (snake == null)
            {
                return;
            }

            _context.AddSnake(snake);
        }

        public async Task RunGames()
        {
            var gamesToRemove = new List<Game>();
            
            foreach (var game in _context.Games)
            {
                if (game.HasNoPlayersAlive())
                {
                    gamesToRemove.Add(game);
                    continue;
                }
                
                game.Run();
                
                await _hub.Clients.Groups(game.Id).SendAsync("Update", game);
            }

            foreach (var game in gamesToRemove)
            {
                _context.RemoveGame(game);
            }
        }

        public void DisconnectPlayer(string snakeId)
        {
            _context.GetSnakeById(snakeId)?.Die();
        }

        public void MoveOrBoost(string snakeId, string direction)
        {
            _context.GetSnakeById(snakeId)?.Move(direction);
        }

        public void StopSnakeBoost(string snakeId)
        {
            _context.GetSnakeById(snakeId)?.ReduceSpeed();
        }

        private Game GetGameById(string gameId)
        {
            return _context.Games.FirstOrDefault(g => g.Id == gameId);
        }
    }
}