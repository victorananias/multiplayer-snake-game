using System;

namespace MultiplayerSnakeGame.Entities
{
    public class SnakePiece
    {
        public SnakePiece(int x, int y)
        {
            X = x;
            Y = y;
            Size = 20;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public Hitbox Hitbox { 
            get 
            {
                return new Hitbox
                {
                    X = this.X,
                    Y = this.Y,
                    Width = 20,
                    Height = 20
                };
            }
        }

        internal void Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}