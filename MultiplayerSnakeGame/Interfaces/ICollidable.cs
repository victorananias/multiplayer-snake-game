using System.Collections.Generic;
using MultiplayerSnakeGame.Entities;

namespace MultiplayerSnakeGame.Interfaces
{
    public interface ICollidable
    {
        List<Hitbox> Hitboxes { get; }
        string Id { get; set; }
        void WillCollideTo(ICollidable collidable);
        void WillBeHittedBy(ICollidable collidable);
        ICollidable Updated();
        bool Is(ICollidable collidable);
    }
}
