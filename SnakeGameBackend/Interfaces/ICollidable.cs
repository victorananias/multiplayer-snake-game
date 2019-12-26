using System.Collections.Generic;
using SnakeGameBackend.Entities;

namespace SnakeGameBackend.Interfaces
{
    public interface ICollidable
    {
        List<Hitbox> Hitboxes { get; }
        string Id { get; set; }

        void CollidedTo(ICollidable collidable);
    }
}
