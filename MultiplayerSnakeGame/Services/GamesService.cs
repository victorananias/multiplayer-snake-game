using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using MultiplayerSnakeGame.Entities;
using MultiplayerSnakeGame.Hubs;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Services
{
    public class GamesService
    {
        public List<Game> Games { get; set; }
        private IHubContext<GameHub> _hub;

        public GamesService(
            CollisorService collisorService,
            IHubContext<GameHub> hub
        )
        {
            _hub = hub;
            Games = new List<Game>();
        }

        public void RunGames()
        {
            Games.ForEach(game => game.Run());
        }

        public void JoinGame(string gameId, string connectionId)
        {
            if (gameId == null)
            {
                gameId = Guid.NewGuid().ToString();
            }

            var game = GetGameById(gameId);

            if (game == null)
            {
                game = new Game(gameId);
                Games.Add(game);
            }

            game.Add(connectionId);
            game.GenerateFruit();

            _hub.Groups.AddToGroupAsync(connectionId, groupName: gameId);
        }

        public void RemovePlayer(string snakeId)
        {
            foreach (var game in Games)
            {
                var snake = game.GetSnakeById(snakeId);

                if (snake == null) continue;

                game.RemoveSnake(snake.Id);
                
                return;
            }
        }

        public void MoveSnake(string gameId, string snakeId, string direction)
        {
            GetGameById(gameId)?.MoveSnake(snakeId, direction);
        }

        public void ReduceSnakeSpeed(string gameId, string snakeId)
        {
            GetGameById(gameId)?.ReduceSnakeSpeed(snakeId);
        }

        private Game GetGameById(string gameId)
        {
            return Games.FirstOrDefault(g => g.Id == gameId);
        }
    }
}