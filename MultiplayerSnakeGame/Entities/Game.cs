using System;
using System.Collections.Generic;
using System.Linq;
using MultiplayerSnakeGame.Interfaces;
using MultiplayerSnakeGame.Services;

namespace MultiplayerSnakeGame.Entities
{
    public class Game
    {
        public string Id { get; set; }
        public List<Player> Players { get; set; }
        public List<Point> Points { get; set; }
        public List<Score> ScoreList { get; set; }

        public bool Over { get; set; }
        public int PointsToWin { get; }

        public Player Winner => Players.FirstOrDefault(s => s.Win);

        public const int PlayersLimit = 5;

        public Game(string gameId)
        {
            Id = gameId;
            PointsToWin = 100;
            Players = new List<Player>();
            Points = new List<Point>();
            ScoreList = new List<Score>();
        }

        public void Run()
        {
            if (!Players.Any())
            {
                return;
            }

            var playersToUpdate = GetPlayersToUpdate();

            CheckPlayersCollisions(playersToUpdate);
            UpdatePlayers(playersToUpdate);

            if (Players.Count > 1)
            {
                if (Points.Count < Players.Count - 1)
                {
                    GeneratePoint();
                }
            }

            SortScore();
        }

        public Player TryCreatePlayer(string playerId)
        {
            var random = new Random();
            var x = random.Next(500 - 20) / 20 * 20;
            var y = random.Next(500 - 20) / 20 * 20;

            if (Players.Count == PlayersLimit)
            {
                return null;
            }

            var player = new Player(playerId, x, y, this);

            Players.Add(player);

            ScoreList.Add(new Score
            {
                PlayerId = playerId
            });

            return player;
        }

        public void GeneratePoint()
        {
            var random = new Random();
            var x = random.Next(500 - 20) / 20 * 20;
            var y = random.Next(500 - 20) / 20 * 20;
            var point = new Point(x, y, this);

            Points.Add(point);
        }

        public void RemovePoint(Point point)
        {
            Points.Remove(point);
        }

        public void PointTo(Player player)
        {
            var score = ScoreList.First(s => s.PlayerId == player.Id);
            score.Points += 10;

            if (score.Points == PointsToWin)
            {
                WinTo(player);
            }
        }

        public bool HasNoPlayersAlive()
        {
            return Players.Count > 0 && !Players.Any(s => s.Alive);
        }

        private void WinTo(Player player)
        {
            Over = true;
            player.Win = true;
        }

        private void CheckPlayersCollisions(List<Player> playersToUpdate)
        {
            var collidables = GetCollidables();

            foreach (var player in playersToUpdate)
            {
                CollisorService.Check(player, collidables);
            }
        }

        private void UpdatePlayers(List<Player> playersToUpdate)
        {
            foreach (var player in playersToUpdate)
            {
                player.Update();
            }
        }

        private void SortScore()
        {
            ScoreList = ScoreList.OrderByDescending(s => s.Points).ToList();
        }

        private List<Player> GetPlayersToUpdate()
        {
            return Players.Where(player => player.Alive && player.ShouldUpdate()).ToList();
        }

        private List<ICollidable> GetCollidables()
        {
            return new List<ICollidable>(Players).Concat(Points).ToList();
        }
    }
}