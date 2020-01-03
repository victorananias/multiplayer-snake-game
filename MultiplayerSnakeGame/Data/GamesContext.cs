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
    }
}
