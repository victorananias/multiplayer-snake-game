using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerSnakeGame.Entities;

namespace MultiplayerSnakeGame.Data
{
    public class GamesContext
    {
        public GamesContext()
        {
            Games = new List<Game>();
            Players = new List<Player>();
        }

        public List<Game> Games { get; set; }
        public List<Player> Players { get; set; }

        public Player GetSnakeById(string playerId)
        {
            return Players.FirstOrDefault(s => s.Id == playerId);
        }

        public void AddGame(Game game)
        {
            Games.Add(game);
        }

        public void RemoveGame(Game game)
        {
            game.Players.ForEach(player => Players.Remove(player));
            Games.Remove(game);
        }

        public Game GetGameById(string gameId)
        {
            return Games.FirstOrDefault(g => g.Id == gameId);
        }

        public void AddSnake(Player player)
        {
            Players.Add(player);
        }

        public Game CreateGame(string gameId)
        {
            var game = new Game(gameId);
            AddGame(game);
            return game;
        }

        public void RemoveGames(List<Game> gamesToRemove)
        {
            foreach (var game in gamesToRemove)
            {
                RemoveGame(game);
            }
        }
    }
}