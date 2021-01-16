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

        public void ConnectPlayer(string gameId, string playerId)
        {
            var game = GetGameById(gameId);

            if (game == null)
            {
                game = _context.CreateGame(gameId);
            }

            var player = game.TryCreatePlayer(playerId);

            if (player == null)
            {
                return;
            }

            _context.AddSnake(player);
            AddPlayerIdToGameIdNotifyList(playerId, gameId);
        }

        private void AddPlayerIdToGameIdNotifyList(string playerId, string gameId)
        {
            _hub.Groups.AddToGroupAsync(playerId, groupName: gameId);
        }

        public async Task ExecuteAsync()
        {
            await RunGamesAsync();
            RemoveNonPlayedGames();
        }

        public void DisconnectPlayer(string playerId)
        {
            _context.GetSnakeById(playerId)?.Die();
        }

        public void MoveOrBoost(string playerId, string direction)
        {
            _context.GetSnakeById(playerId)?.MoveOrBoost(direction);
        }

        public void StopSnakeBoost(string playerId)
        {
            _context.GetSnakeById(playerId)?.ReduceSpeed();
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

                if (game.Over)
                {
                    await NotifyClientsGameIsOver(game);
                    _gamesToRemove.Add(game);
                }

                game.Run();

                await NotifyClientsGameUpdates(game);
            }
        }

        private async Task NotifyClientsGameIsOver(Game game)
        {
            await _hub.Clients.Client(game.Winner.Id).SendAsync("Win");
            await _hub.Groups.RemoveFromGroupAsync(game.Winner.Id, game.Id);
            await _hub.Clients.Groups(game.Id).SendAsync("Lose");
        }

        private async Task NotifyClientsGameUpdates(Game game)
        {
            await _hub.Clients.Groups(game.Id).SendAsync("Update", game);
        }

        private void RemoveNonPlayedGames()
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