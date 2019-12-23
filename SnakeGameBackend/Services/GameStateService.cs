using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGameBackend.Entities;

namespace SnakeGameBackend.Services
{
    public class GameStateService
    {
        public GameState State { get; set; }

        public GameStateService()
        {
            State = new GameState
            {
                Snakes = new List<Snake>(),
                Fruits = new List<Fruit>()
            };
        }

        public void AddSnake(string id)
        {
            var snake = new Snake(id, 200, 100);

            State.Snakes.Add(snake);
        }

        public void GenerateFruit()
        {
            var random = new Random();
            var x = (int) (random.Next(500 - 20) / 20) * 20;
            var y = (int) (random.Next(500 - 20) / 20) * 20;

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
    }
}