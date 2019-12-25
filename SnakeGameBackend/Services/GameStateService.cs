using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGameBackend.Entities;

namespace SnakeGameBackend.Services
{
    public class GameStateService
    {
        public GameStateService()
        {
            State = new GameState
            {
                Snakes = new List<Snake>(),
                Fruits = new List<Fruit>()
            };
        }

        public GameState State { get; set; }

        public void AddSnake(string id)
        {
            var snake = new Snake(id, 200, 100);

            State.Snakes.Add(snake);
        }

        public void GenerateFruit()
        {
            var random = new Random();
            var x = random.Next(500 - 20) / 20 * 20;
            var y = random.Next(500 - 20) / 20 * 20;

            State.Fruits.Add(new Fruit(x, y));
        }

        internal void MoveSnake(string connectionId, string direction)
        {
            State.Snakes.First(s => s.Id == connectionId).Direction = direction;
        }

        public void RemoveSnake(string id)
        {
            State.Snakes.Remove(State.Snakes.Find(s => s.Id == id));
        }

        public void RemoveFruit(string id)
        {
            State.Fruits.Remove(State.Fruits.Find(s => s.Id == id));
        }

        public void Collide(ICollidable collidable1, ICollidable collidable2)
        {
            collidable1.CollidedTo(collidable2);
            collidable1.CollidedTo(collidable1);

            var fruit = collidable1.GetType() == typeof(Snake) ? collidable2 : collidable1;

            RemoveFruit(fruit.Id);
            GenerateFruit();
        }
    }
}