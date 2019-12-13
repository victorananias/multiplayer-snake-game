using System.Collections.Generic;
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
                Fruit = new Fruit() { X = 200, Y = 200 }
            };
        }

        public void AddSnake(string id)
        {
            this.State.Snakes.Add(new Snake(id, 300, 300));
        }

        public void RemoveSnake(string id)
        {
            this.State.Snakes.Remove(this.State.Snakes.Find(s => s.Id == id));
        }
    }
}