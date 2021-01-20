using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerSnakeGame.Entities;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Services
{
    public class CollisorService
    {
        public static void Check(ICollidable collidable, List<ICollidable> collidables)
        {
            var collidable1 = collidable.Updated();

            foreach (var collidable2 in collidables)
            {
                if (collidable1.Is(collidable2))
                {
                    continue;
                }

                var collided = from hitbox1 in collidable1.Hitboxes
                    from hitbox2 in collidable2.Hitboxes
                    where (
                        hitbox1.X >= hitbox2.X
                        && hitbox1.X + hitbox1.Width <= hitbox2.X + hitbox2.Width
                        && hitbox1.Y >= hitbox2.Y
                        && hitbox1.Y + hitbox1.Height <= hitbox2.Y + hitbox1.Height
                    )
                    select hitbox1;

                if (collided.Any())
                {
                    collidable.WillCollideTo(collidable2);
                    collidable2.WillBeHittedBy(collidable);
                }
            }
        }
    }
}
