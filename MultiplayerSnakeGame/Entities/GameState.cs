using System.Collections.Generic;

namespace MultiplayerSnakeGame.Entities
{
    public class GameState
    {
        public List<Snake> Snakes { get; set; }
        public List<Fruit> Fruits { get; set; }
        public List<Score> ScoreList { get; set; }
    }
}