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
        public List<Snake> Snakes { get; set; }
        public List<Fruit> Fruits { get; set; }
        public List<Score> ScoreList { get; set; }
        private readonly int _playersLimit;

        private readonly CollisorService _collisorService;

        public Game(string gameId)
        {
            _collisorService = new CollisorService();

            Id = gameId;
            Snakes = new List<Snake>();
            Fruits = new List<Fruit>();
            ScoreList = new List<Score>();
            _playersLimit = 5;
        }

        public void Run()
        {
            if (!Snakes.Any())
            {
                return;
            }

            var collidables = new List<ICollidable>(Snakes);
            collidables.AddRange(Fruits);

            foreach (var snake in Snakes.Where(snake => snake.Alive && snake.ShouldUpdate()))
            {
                _collisorService.Check(snake, collidables);
            }

            foreach (var snake in Snakes.Where(snake => snake.Alive && snake.ShouldUpdate()))
            {
                snake.Update();
            }
        }

        public Snake CreateSnake(string snakeId)
        {
            var random = new Random();
            var x = random.Next(500 - 20) / 20 * 20;
            var y = random.Next(500 - 20) / 20 * 20;

            if (Snakes.Count == _playersLimit)
            {
                return null;
            }

            var snake = new Snake(snakeId, x, y, this);

            Snakes.Add(snake);

            ScoreList.Add(new Score
            {
                SnakeId = snakeId
            });

            return snake;
        }

        public void GenerateFruit()
        {
            var random = new Random();
            var x = random.Next(500 - 20) / 20 * 20;
            var y = random.Next(500 - 20) / 20 * 20;
            var fruit = new Fruit(x, y, this);

            Fruits.Add(fruit);
        }

        public void RemoveFruit(Fruit fruit)
        {
            Fruits.Remove(fruit);
        }

        public void PointTo(Snake snake)
        {
            ScoreList.First(s => s.SnakeId == snake.Id).Points += 10;
        }

        public bool HasNoPlayersAlive()
        {
            return Snakes.Count > 0 && !Snakes.Any(s => s.Alive);
        }
    }
}