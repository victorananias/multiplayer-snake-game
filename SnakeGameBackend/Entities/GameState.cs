using System.Collections.Generic;

namespace SnakeGameBackend.Entities
{
    public class GameState
    {
        public List<Snake> Snakes { get; set; }
        public List<Fruit> Fruits { get; set; }
    }
}