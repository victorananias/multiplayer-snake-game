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

        private readonly int _playersLimit;
        private readonly CollisorService _collisorService;

        public Game(string gameId)
        {
            _collisorService = new CollisorService();

            Id = gameId;
            PointsToWin = 100;
            Players = new List<Player>();
            Points = new List<Point>();
            ScoreList = new List<Score>();
            _playersLimit = 5;
        }

        public void Run()
        {
            if (!Players.Any())
            {
                return;
            }

            var collidables = new List<ICollidable>(Players);
            collidables.AddRange(Points);

            foreach (var player in Players.Where(player => player.Alive && player.ShouldUpdate()))
            {
                _collisorService.Check(player, collidables);
            }

            foreach (var player in Players.Where(player => player.Alive && player.ShouldUpdate()))
            {
                player.Update();
            }

            if (Players.Count > 1)
            {
                if (Points.Count < Players.Count - 1)
                {
                    GeneratePoint();
                }
            }
            
            ScoreList = ScoreList.OrderByDescending(s => s.Points).ToList();
        }

        public Player TryCreatePlayer(string playerId)
        {
            var random = new Random();
            var x = random.Next(500 - 20) / 20 * 20;
            var y = random.Next(500 - 20) / 20 * 20;

            if (Players.Count == _playersLimit)
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

        private void WinTo(Player player)
        {
            Over = true;
            player.Win = true;
        }

        public bool HasNoPlayersAlive()
        {
            return Players.Count > 0 && !Players.Any(s => s.Alive);
        }
    }
}