using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using MultiplayerSnakeGame.Entities;
using MultiplayerSnakeGame.Hubs;

namespace MultiplayerSnakeGame.Services
{
    public class GamesService
    {
        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }
        private IHubContext<GameHub> _hub;

        public GamesService(
            CollisorService collisorService,
            IHubContext<GameHub> hub
        )
        {
            _hub = hub;
            Games = new List<Game>();
            Players = new List<Player>();
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
                Games.Add(game);
            }

            AddToPlayersList(gameId, playerId);

            game.Add(playerId);
            game.GenerateFruit();

            _hub.Groups.AddToGroupAsync(playerId, groupName: gameId);
        }

        private void AddToPlayersList(string gameId, string playerId)
        {
            Players.Add(new Player
            {
                Id = playerId,
                GameId = gameId
            });
        }

        public void RemovePlayer(string playerId)
        {
            var playerInfo = Players.SingleOrDefault(p => p.Id == playerId);
            var game = Games.SingleOrDefault(g => g.Id == playerInfo.GameId);
                
            game.RemoveSnakeById(playerId);

            if (!game.Snakes.Any())
            {
                Games.Remove(game);
            }

            Console.WriteLine($"There are {Games.Count} games being played.");
        }

        public void MoveSnake(string playerId, string direction)
        {
            var player = Players.FirstOrDefault(p => p.Id == playerId);
            GetGameById(player.GameId)?.MoveSnake(playerId, direction);
        }

        public void ReduceSnakeSpeed(string playerId)
        {
            var player = Players.FirstOrDefault(p => p.Id == playerId);
            GetGameById(player.GameId)?.ReduceSnakeSpeed(playerId);
        }

        private Game GetGameById(string gameId)
        {
            return Games.FirstOrDefault(g => g.Id == gameId);
        }
    }
}