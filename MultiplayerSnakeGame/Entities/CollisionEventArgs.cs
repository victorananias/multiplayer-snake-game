using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Entities
{
    public class CollisionEventArgs
    {
        public ICollidable[] Collidables { get; set; }
    }
}