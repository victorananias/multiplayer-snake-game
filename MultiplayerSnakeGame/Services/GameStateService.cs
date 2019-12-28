using System;
using System.Collections.Generic;
using System.Linq;
using MultiplayerSnakeGame.Entities;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Services
{
    public class GameStateService
    {

        public int PlayersLimit;
        public GameState State { get; set; }

        public GameStateService()
        {
            State = new GameState
            {
                Snakes = new List<Snake>(),
                Fruits = new List<Fruit>(),
                ScoreList = new List<Score>()
            };
            PlayersLimit = 2;
        }

        public void AddSnake(string id)
        {
            if (State.Snakes.Count() == PlayersLimit)
            {
                return;
            }

            var snake = new Snake(id, 200, 100);

            State.Snakes.Add(snake);
            State.ScoreList.Add(new Score
            {
                PlayerId = id,
                Points = 0
            });
        }

        internal void ResetState()
        {
            State = new GameState
            {
                Snakes = new List<Snake>(),
                Fruits = new List<Fruit>(),
                ScoreList = new List<Score>()
            };
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
            GetSnakeById(connectionId)?.Move(direction);
        }

        internal void ReduceSnakeSpeed(string connectionId)
        {
            GetSnakeById(connectionId)?.ReduceSpeed();
        }

        public void RemoveSnake(string id)
        {
            State.Snakes.Remove(GetSnakeById(id));
        }

        public void RemoveFruit(string id)
        {
            State.Fruits.Remove(GetFruitById(id));
        }

        public void PointTo(string id)
        {
            var score = State.ScoreList.FirstOrDefault(s => s.PlayerId == id);

            if (score == null)
            {
                State.ScoreList.Add(new Score
                {
                    PlayerId = id,
                    Points = 10
                });

                return;
            }

            score.Points += 10;

            State.ScoreList = State.ScoreList.OrderByDescending(s => s.Points).ToList();
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