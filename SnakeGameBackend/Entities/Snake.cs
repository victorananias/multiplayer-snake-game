using System;
using System.Collections.Generic;

namespace SnakeGameBackend.Entities
{
    public class Snake
    {
        public Snake(string id, int x, int y)
        {
            Id = id;
            Head = new SnakePiece(x, y);
            Pieces = new List<SnakePiece>();
            LastUpdate = DateTime.Now;
            ShouldGrow = false;
        }

        public string Id { get; set; }
        public SnakePiece Head { get; set; }
        public List<SnakePiece> Pieces { get; set; }
        public bool ShouldGrow { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}