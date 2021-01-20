using System.Collections.Generic;

namespace MultiplayerSnakeGame.Data
{
    public abstract class MoveKeys
    {
        public const string Up = "Up";
        public const string Right = "Right";
        public const string Down = "Down";
        public const string Left = "Left";

        public static List<string> All()
        {
            return new List<string> { Up, Right, Down, Left };
        }
    }
}