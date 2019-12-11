using System.Collections.Generic;

namespace SnakeGameBackend.Entities
{
    public class GameState
    {
        public List<Snake> snakes { get; set; }
        public Fruit fruit { get; set; }
    }
}