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
        private List<Game> _gamesToRemove = new List<Game>();

        public GamesService(
            GamesContext context,
            IHubContext<GameHub> hub
        )
        {
            _context = context;
            _hub = hub;
        }

        public void ConnectPlayer(string gameId, string snakeId)
        {
            var game = GetGameById(gameId);

            if (game == null)
            {
                game = _context.CreateGame(gameId);
            }

            var snake = game.TryCreateSnake(snakeId);

            if (snake == null)
            {
                return;
            }

            _context.AddSnake(snake);
            _hub.Groups.AddToGroupAsync(snakeId, groupName: gameId);
        }

        public async Task RunAsync()
        {
            await RunGamesAsync();
            RemoveGames();
        }

        public void DisconnectPlayer(string snakeId)
        {
            _context.GetSnakeById(snakeId)?.Die();
        }

        public void MoveOrBoost(string snakeId, string direction)
        {
            _context.GetSnakeById(snakeId)?.MoveOrBoost(direction);
        }

        public void StopSnakeBoost(string snakeId)
        {
            _context.GetSnakeById(snakeId)?.ReduceSpeed();
        }

        private async Task RunGamesAsync()
        {
            foreach (var game in _context.Games)
            {
                if (game.HasNoPlayersAlive())
                {
                    _gamesToRemove.Add(game);
                    continue;
                }

                await CheckIfGameHasOverAsync(game);

                game.Run();

                await UpdateGameAsync(game);
            }
        }

        private async Task CheckIfGameHasOverAsync(Game game)
        {
            if (!game.Over)
            {
                return;
            }

            var winner = game.Winner;
            await _hub.Clients.Client(winner.Id).SendAsync("Win");
            await _hub.Groups.RemoveFromGroupAsync(winner.Id, game.Id);
            await _hub.Clients.Groups(game.Id).SendAsync("Lose");
            _gamesToRemove.Add(game);
        }

        private async Task UpdateGameAsync(Game game)
        {
            await _hub.Clients.Groups(game.Id).SendAsync("Update", game);
        }

        private void RemoveGames()
        {
            foreach (var game in _gamesToRemove)
            {
                _context.RemoveGame(game);
            }
        }

        private Game GetGameById(string gameId)
        {
            return _context.Games.FirstOrDefault(g => g.Id == gameId);
        }
    }
}