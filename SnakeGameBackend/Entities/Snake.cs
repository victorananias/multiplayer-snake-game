using System;
using System.Collections.Generic;
using System.Linq;
using SnakeGameBackend.Interfaces;

namespace SnakeGameBackend.Entities
{
    public class Snake: ICollidable
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
        public void CollidedTo(ICollidable collidable)
        {
            if (collidable.GetType() == typeof(Fruit))
            {
                Grow();
            }
        }

        public SnakePiece Head { get; set; }
        public List<SnakePiece> Body { get; set; }
        public bool ShouldGrow { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Direction { get; set; }

        public List<Hitbox> Hitboxes
        {
            get {
                var hitboxes = new List<Hitbox>() { Head.Hitbox };
                hitboxes.AddRange(Body.Select(p => p.Hitbox).ToList());
                return hitboxes;
            }
        }
        

        internal void Update()
        {
            var y = Head.Y;
            var x = Head.X;

            switch (Direction)
            {
                case "up":
                    y -= Head.Size;
                    if (y < 0)
                    {
                        y = 500 - 20;
                    }

                    break;
                case "right":
                    x += Head.Size;
                    if (x > 500 - 20)
                    {
                        x = 0;
                    }
                    break;

                case "down":
                    y += Head.Size;
                    if (y > 500 - 20)
                    {
                        y = 0;
                    }
                    break;

                case "left":
                    x -= Head.Size;
                    if (x < 0)
                    {
                        x = 500 - 20;
                    }
                    break;
                default:
                    return;
            }

            var piece = Head;

            var oldX = piece.X;
            var oldY = piece.Y;

            piece.Move(x, y);

            x = oldX;
            y = oldY;

            for (var i = 0; i < Body.Count; i++)
            {
                piece = Body[i];

                oldX = piece.X;
                oldY = piece.Y;

                piece.Move(x, y);

                x = oldX;
                y = oldY;
            }
        }

        internal void Grow()
        {
            Body.Add(new SnakePiece(-200,-200));
        }
    }
}