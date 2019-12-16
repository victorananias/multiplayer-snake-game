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
                Fruits = new List<Fruit>
                {
                    new Fruit { X = 200, Y = 200 }
                } 
            };
        }

        public void AddSnake(string id)
        {
            var snake = new Snake(id, 0, 100);

            State.Snakes.Add(snake);
        }

        internal void MoveSnake(string connectionId, string direction)
        {
            State.Snakes.First(s => s.Id == connectionId).Direction = direction;
        }

        public void RemoveSnake(string id)
        {
            State.Snakes.Remove(State.Snakes.Find(s => s.Id == id));
        }
    }
}