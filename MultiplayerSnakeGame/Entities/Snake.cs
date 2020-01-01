using System;
using System.Collections.Generic;
using System.Linq;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Entities
{
    public class Snake: ICollidable
    {
        public Snake(string id, string gameId, int x, int y)
        {
            Id = id;
            GameId = gameId;
            Head = new SnakePiece(x, y);
            Body = new List<SnakePiece>();
            LastUpdate = DateTime.Now;
            Direction = "";
            DefaultUpdateTime = 300;
            CurrentUpdateTime = DefaultUpdateTime;
            Alive = true;
        }

        public int DefaultUpdateTime { get; set; }
        public int CurrentUpdateTime { get; set; }
        public string Id { get; set; }
        public string GameId { get; set; }
        public SnakePiece Head { get; set; }
        public List<SnakePiece> Body { get; set; }
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
        public bool Alive { get; set; }

        public void Move(string direction)
        {
            if (
                (direction == "up" && Direction == "down")
                || (direction == "down" && Direction == "up")
                || (direction == "right" && Direction == "left")
                || (direction == "left" && Direction == "right")
            )
            {
                return;
            }

            Direction = direction;
            CurrentUpdateTime = 100;
        }

        public void ReduceSpeed()
        {
            CurrentUpdateTime = DefaultUpdateTime;
        }

        public void CollidedTo(ICollidable collidable)
        {
            if (collidable.GetType() == typeof(Fruit))
            {
                Grow();
            }
        }


        public void Update()
        {
            if (!Alive)
            {
                return;
            }

            if (!ShouldUpdate())
            {
                return;
            }

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

        public void Die()
        {
            Alive = false;
        }

        private void Grow()
        {
            Body.Add(new SnakePiece(-200,-200));
        }

        private bool ShouldUpdate()
        {
            var now = DateTime.Now;
      
            if (now.Subtract(LastUpdate).TotalMilliseconds <= CurrentUpdateTime)
            {
                return false;
            }

            LastUpdate = now;

            return true;
        }
    }
}