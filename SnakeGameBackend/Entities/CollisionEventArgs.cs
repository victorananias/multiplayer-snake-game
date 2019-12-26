using SnakeGameBackend.Interfaces;

namespace SnakeGameBackend.Entities
{
    public class CollisionEventArgs
    {
        public ICollidable Collidable1 { get; set; }
        public ICollidable Collidable2 { get; set; }
    }
}