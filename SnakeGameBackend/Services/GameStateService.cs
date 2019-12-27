using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGameBackend.Entities;
using SnakeGameBackend.Interfaces;

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
            GetSnakeById(connectionId).Move(direction);
        }

        internal void ReduceSnakeSpeed(string connectionId)
        {
            GetSnakeById(connectionId).ReduceSpeed();
        }

        public void RemoveSnake(string id)
        {
            State.Snakes.Remove(GetSnakeById(id));
        }

        public void RemoveFruit(string id)
        {
            State.Fruits.Remove(GetFruitById(id));
        }

        private Snake GetSnakeById(string id)
        {
            return State.Snakes.Find(s => s.Id == id);
        }
        private Fruit GetFruitById(string id)
        {
            return State.Fruits.Find(s => s.Id == id);
        }
    }
}