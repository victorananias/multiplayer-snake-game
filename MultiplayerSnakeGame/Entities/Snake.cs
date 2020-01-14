using System;
using System.Collections.Generic;
using System.Linq;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Entities
{
    public class Snake : ICollidable
    {
        public Snake(string id, int x, int y, Game game)
        {
            Id = id;
            Head = new SnakePiece(x, y);
            Body = new List<SnakePiece>();
            _game = game;
            LastUpdate = DateTime.Now;
            Direction = "";
            DefaultUpdateTime = 300;
            CurrentUpdateTime = DefaultUpdateTime;
            Alive = true;
        }

        public int DefaultUpdateTime { get; set; }
        public int CurrentUpdateTime { get; set; }
        public string Id { get; set; }
        public SnakePiece Head { get; set; }
        public List<SnakePiece> Body { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Direction { get; set; }

        public List<Hitbox> Hitboxes
        {
            get
            {
                var hitboxes = new List<Hitbox>() {Head.Hitbox};
                hitboxes.AddRange(Body.Select(p => p.Hitbox).ToList());
                return hitboxes;
            }
        }

        public bool Alive { get; set; }
        public bool ShouldGrow { get; set; }
        public bool Win { get; set; }

        private Game _game;

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

        public ICollidable Next()
        {
            var updatedSnake = new Snake(Id, Head.X, Head.Y, null)
            {
                Body = Body.Select(b => new SnakePiece(b.X, b.Y)).ToList(),
                LastUpdate = DateTime.Now.AddMinutes(-1),
                Direction = Direction
            };

            updatedSnake.Update();

            return updatedSnake;
        }

        public void Update()
        {
            if (ShouldGrow)
            {
                Grow();
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

            LastUpdate = DateTime.Now;
        }

        public void Die()
        {
            Alive = false;
        }

        public void Grow()
        {
            Body.Add(new SnakePiece(-200, -200));
        }

        public bool ShouldUpdate()
        {
            return !(DateTime.Now.Subtract(LastUpdate).TotalMilliseconds <= CurrentUpdateTime);
        }

        public void WillCollideTo(ICollidable collidable)
        {
            if (collidable is Snake)
            {
                Die();
            }

            
            if (collidable is Fruit)
            {
                Grow();
                _game.PointTo(this);
            }
        }

        public void WillBeHittedBy(ICollidable collidable)
        {
        }
    }
}