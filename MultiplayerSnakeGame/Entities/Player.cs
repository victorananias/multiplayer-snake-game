using System;
using System.Collections.Generic;
using System.Linq;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Entities
{
    public class Player : ICollidable
    {
        public Player(string id, int x, int y, Game game)
        {
            Id = id;
            Head = new PlayerHitbox(x, y);
            Body = new List<PlayerHitbox>();
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
        public PlayerHitbox Head { get; set; }
        public List<PlayerHitbox> Body { get; set; }
        public DateTime LastUpdate { get; set; }
        public string Direction { get; set; }

        public List<Hitbox> Hitboxes
        {
            get
            {
                var hitboxes = new List<Hitbox>() { Head.Hitbox };
                hitboxes.AddRange(Body.Select(p => p.Hitbox).ToList());
                return hitboxes;
            }
        }

        public bool Alive { get; set; }
        public bool ShouldGrow { get; set; }
        public bool Win { get; set; }

        private Game _game;

        public void MoveOrBoost(string direction)
        {
            if (IsCurrentDirectionOpositeToReceived(direction))
            {
                return;
            }

            Direction = direction;
            CurrentUpdateTime = 100;
        }

        private bool IsCurrentDirectionOpositeToReceived(string direction)
        {
            return (direction == "up" && Direction == "down")
                    || (direction == "down" && Direction == "up")
                    || (direction == "right" && Direction == "left")
                    || (direction == "left" && Direction == "right");
        }

        public void ReduceSpeed()
        {
            CurrentUpdateTime = DefaultUpdateTime;
        }

        public ICollidable Next()
        {
            var updatedPlayer = new Player(Id, Head.X, Head.Y, null)
            {
                Body = Body.Select(b => new PlayerHitbox(b.X, b.Y)).ToList(),
                LastUpdate = DateTime.Now.AddMinutes(-1),
                Direction = Direction
            };

            updatedPlayer.Update();

            return updatedPlayer;
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
            Body.Add(new PlayerHitbox(-200, -200));
        }

        public bool ShouldUpdate()
        {
            return !(DateTime.Now.Subtract(LastUpdate).TotalMilliseconds <= CurrentUpdateTime);
        }

        public void WillCollideTo(ICollidable collidable)
        {
            if (collidable is Player)
            {
                Die();
            }


            if (collidable is Point)
            {
                Grow();
                _game.PointTo(this);
            }
        }

        public void WillBeHittedBy(ICollidable collidable)
        {
        }

        public bool Is(ICollidable collidable)
        {
            return Id == collidable.Id;
        }
    }
}