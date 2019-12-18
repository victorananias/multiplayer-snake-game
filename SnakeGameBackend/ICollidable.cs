using SnakeGameBackend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeGameBackend
{
    public class ICollidable
    {
        public List<Hitbox> Hitboxes { get; }
    }
}
