using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Entities
{
    public class Point : ICollidable
    {
        public string Id { get; set; }
        [JsonIgnore]
        private Game Game { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }

        public List<Hitbox> Hitboxes
        {
            get
            {
                return new List<Hitbox>
                {
                    new Hitbox
                    {
                        X = X,
                        Y = Y,
                        Width = 20,
                        Height = 20
                    }
                };
            }
        }


        public Point(Game game)
        {
            Size = 20;
            Id = Guid.NewGuid().ToString();
            Game = game;
        }

        public Point(int x, int y, Game game)
        {
            Size = 20;
            Id = Guid.NewGuid().ToString();
            X = x;
            Y = y;
            Game = game;
        }

        public ICollidable Updated()
        {
            return this;
        }

        public void WillCollideTo(ICollidable collidable)
        {
        }

        public void WillBeHittedBy(ICollidable collidable)
        {
            Game.RemovePoint(this);
        }

        public bool Is(ICollidable collidable)
        {
            return Id == collidable.Id;
        }
    }
}