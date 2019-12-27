using SnakeGameBackend.Interfaces;

namespace SnakeGameBackend.Entities
{
    public class CollisionEventArgs
    {
        public ICollidable[] Collidables { get; set; }
    }
}