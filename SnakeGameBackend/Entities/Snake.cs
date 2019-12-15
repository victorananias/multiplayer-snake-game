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
            Direction = "";
        }

        public string Id { get; set; }
        public SnakePiece Head { get; set; }
        public List<SnakePiece> Pieces { get; set; }
        public bool ShouldGrow { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Direction { get; set; }

        internal void Update()
        {
            switch (Direction)
            {
                case "up":
                    Head.Y -= Head.Size;
                    break;
                case "right":
                    Head.X += Head.Size;
                    break;

                case "down":
                    Head.Y += Head.Size;
                    break;

                case "left":
                    Head.X -= Head.Size;
                    break;
            }
        }
    }
}