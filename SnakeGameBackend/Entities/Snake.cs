using System.Collections.Generic;

namespace SnakeGameBackend.Entities
{
    public class Snake
    {
        public SnakePiece head { get; set; }
        public List<SnakePiece> pieces { get; set; }
        public float speed { get; set; }
    }
}