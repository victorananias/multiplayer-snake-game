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
        private List<ICollidable> _collidables;
        public event EventHandler<CollisionEventArgs> OnCollision;

        public CollisorService()
        {
            _collidables = new List<ICollidable>();
        }

        public void AddCollidable(ICollidable collidable)
        {
            _collidables.Add(collidable);
        }

        public void RemoveCollidable(ICollidable collidable)
        {
            _collidables.Remove(collidable);
        }

        public void Check(ICollidable collidable1)
        {
            var colliadablesChecked = new Dictionary<string, List<string>>();

            foreach (var collidable2 in _collidables)
            {
                if (collidable1.Id == collidable2.Id)
                {
                    continue;
                }

                if (!colliadablesChecked.ContainsKey(collidable1.Id))
                {
                    colliadablesChecked[collidable1.Id] = new List<string>();
                }

                if (!colliadablesChecked.ContainsKey(collidable2.Id))
                {
                    colliadablesChecked[collidable2.Id] = new List<string>();
                }

                if (
                    colliadablesChecked[collidable1.Id].Contains(collidable2.Id)
                    || colliadablesChecked[collidable2.Id].Contains(collidable1.Id)
                )
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
                    OnCollision?.Invoke(this, new CollisionEventArgs
                    {
                        Collidables = new [] {collidable1, collidable2}
                    });
                    
                    collidable1.CollidedTo(collidable2);
                    collidable2.CollidedTo(collidable1);
                }

                colliadablesChecked[collidable1.Id].Add(collidable2.Id);
                colliadablesChecked[collidable2.Id].Add(collidable1.Id);
            }
        }
    }
}
