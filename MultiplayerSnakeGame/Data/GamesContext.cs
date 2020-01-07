using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerSnakeGame.Entities;

namespace MultiplayerSnakeGame.Data
{
    public class GamesContext
    {
        public GamesContext()
        {
            Games = new List<Game>();
            Snakes = new List<Snake>();
        }

        public List<Game> Games { get; set; }
        public List<Snake> Snakes { get; set; }

        public Snake GetSnakeById(string snakeId)
        {
            return Snakes.FirstOrDefault(s => s.Id == snakeId);
        }

        public void AddGame(Game game)
        {
            Games.Add(game);
            Console.WriteLine($"Game {game.Id} started.");
            Console.WriteLine($"{Games.Count} games being played.");
        }

        public void RemoveGame(Game game)
        {
            game.Snakes.ForEach(snake => Snakes.Remove(snake));
            Games.Remove(game);

            Console.WriteLine($"Game {game.Id} finished.");
            Console.WriteLine($"{Games.Count} games being played.");
        }

        public void AddSnake(Snake snake)
        {
            Snakes.Add(snake);
        }
    }
}