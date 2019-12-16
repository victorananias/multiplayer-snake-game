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
            Body = new List<SnakePiece>();
            LastUpdate = DateTime.Now;
            ShouldGrow = false;
            Direction = "";
        }

        public string Id { get; set; }
        public SnakePiece Head { get; set; }
        public List<SnakePiece> Body { get; set; }
        public bool ShouldGrow { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Direction { get; set; }

        internal void Update()
        {
            switch (Direction)
            {
                case "up":
                    Head.Y -= Head.Size;
                    if (Head.Y < 0)
                    {
                        Head.Y = 500;
                    }

                    break;
                case "right":
                    Head.X += Head.Size;
                    if (Head.X > 500)
                    {
                        Head.X = 0;
                    }
                    break;

                case "down":
                    Head.Y += Head.Size;
                    if (Head.Y > 500)
                    {
                        Head.Y = 0;
                    }
                    break;

                case "left":
                    Head.X -= Head.Size;
                    if (Head.X < 0)
                    {
                        Head.X = 500;
                    }
                    break;
            }
        }
    }
}