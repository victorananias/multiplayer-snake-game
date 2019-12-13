using System.Collections.Generic;

namespace SnakeGameBackend.Entities
{
    public class GameState
    {
        public List<Snake> Snakes { get; set; }
        public Fruit Fruit { get; set; }
    }
}