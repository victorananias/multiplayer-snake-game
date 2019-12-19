using SnakeGameBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGameBackend
{
    public interface ICollidable
    {
        List<Hitbox> Hitboxes { get; }
        string Id { get; set; }
    }
}
