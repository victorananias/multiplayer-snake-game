using System;
using System.Collections.Generic;

namespace SnakeGameBackend.Entities
{
    public class Fruit: ICollidable
    {
        public Fruit()
        {
            Size = 20;
            Id = Guid.NewGuid().ToString();
        }

        public Fruit(int x, int y)
        {
            Size = 20;
            Id = Guid.NewGuid().ToString();
            X = x;
            Y = y;
        }

        public string Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public List<Hitbox> Hitboxes =>
            new List<Hitbox>()
            {
                new Hitbox
                {
                    X = this.X,
                    Y = this.Y,
                    Width = 20,
                    Height = 20
                }
            };
    }
}