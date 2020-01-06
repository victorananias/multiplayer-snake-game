using System;
using System.Collections.Generic;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Entities
{
    public class Fruit : ICollidable
    {
        public string Id { get; set; }
        private Game _game;
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


        public Fruit(Game game)
        {
            Size = 20;
            Id = Guid.NewGuid().ToString();
            _game = game;
        }

        public Fruit(int x, int y, Game game)
        {
            Size = 20;
            Id = Guid.NewGuid().ToString();
            X = x;
            Y = y;
            _game = game;
        }

        public ICollidable Next()
        {
            return this;
        }

        public void WillCollideTo(ICollidable collidable)
        {
        }

        public void WillBeHittedBy(ICollidable collidable)
        {
            _game.RemoveFruit(this);
            _game.GenerateFruit();
        }
    }
}