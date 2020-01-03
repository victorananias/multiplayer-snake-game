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
        public int PlayersLimit { get; set; }

        private readonly CollisorService _collisorService;

        public Game(string gameId)
        {
            _collisorService = new CollisorService();
            _collisorService.OnCollision += OnCollision;

            Id = gameId;
            Snakes = new List<Snake>();
            Fruits = new List<Fruit>();
            ScoreList = new List<Score>();
            PlayersLimit = 2;
        }

        public void Run()
        {
            if (!Snakes.Any())
            {
                return;
            }

            Snakes.ForEach(snake => {
                snake.Update();
                _collisorService.Check(snake);
            });
        }

        public Snake CreateSnake(string snakeId)
        {
            var random = new Random();
            var x = random.Next(500 - 20) / 20 * 20;
            var y = random.Next(500 - 20) / 20 * 20;

            if (Snakes.Count == PlayersLimit)
            {
                return null;
            }

            var snake = new Snake(snakeId, Id, x, y);

            Snakes.Add(snake);
            _collisorService.AddCollidable(snake);

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
            var fruit = new Fruit(x, y);

            Fruits.Add(fruit);
            _collisorService.AddCollidable(fruit);
        }

        private void OnCollision(object collisorService, CollisionEventArgs args)
        {
            var collidables = args.Collidables;
            var fruit = collidables.FirstOrDefault(c => c.GetType() == typeof(Fruit));

            if (fruit != null)
            {
                var snake = collidables.FirstOrDefault(c => c.GetType() == typeof(Snake));

                RemoveFruitById(fruit?.Id);
                // PointTo(snake?.Id);
                // GenerateFruit();
                return;
            }

            // var collided1 = CollidedTo(collidables[0], collidables[1]);
            // var collided2 = CollidedTo(collidables[1], collidables[0]);

            // if (collided1)
            // {
            //     ((Snake) collidables[0]).Die();
            // }

            // if (collided2)
            // {
            //     ((Snake) collidables[1]).Die();
            // }
        }

        private bool CollidedTo(ICollidable collidable1, ICollidable collidable2)
        {
            var hitbox1 = collidable1.Hitboxes.First();

            var collided = from hitbox2 in collidable2.Hitboxes
                where (
                    hitbox1.X >= hitbox2.X
                    && hitbox1.X + hitbox1.Width <= hitbox2.X + hitbox2.Width
                    && hitbox1.Y >= hitbox2.Y

                    && hitbox1.Y + hitbox1.Height <= hitbox2.Y + hitbox1.Height
                )
                select hitbox2;

            return collided.Any();
        }

        public void RemoveFruitById(string id)
        {
            var fruit = GetFruitById(id);
            Fruits.Remove(fruit);
            // _collisorService.RemoveCollidable(fruit);
        }

        public void MoveSnake(string connectionId, string direction)
        {
            GetSnakeById(connectionId)?.Move(direction);
        }

        public void ReduceSnakeSpeed(string connectionId)
        {
            GetSnakeById(connectionId)?.ReduceSpeed();
        }

        private void PointTo(string id)
        {
            var score = ScoreList.FirstOrDefault(s => s.SnakeId == id);

            if (score == null)
            {
                ScoreList.Add(new Score
                {
                    SnakeId = id,
                    Points = 10
                });

                return;
            }

            score.Points += 10;

            ScoreList = ScoreList.OrderByDescending(s => s.Points).ToList();
        }

        public Snake GetSnakeById(string id)
        {
            return Snakes.Find(s => s.Id == id);
        }

        private Fruit GetFruitById(string id)
        {
            return Fruits.Find(s => s.Id == id);
        }
    }
}
