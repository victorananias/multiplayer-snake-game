using System;
using System.Collections.Generic;
using System.Linq;
using MultiplayerSnakeGame.Entities;
using MultiplayerSnakeGame.Interfaces;

namespace MultiplayerSnakeGame.Services
{
    public class GameService
    {
        private readonly GameStateService _gameState;
        private readonly CollisorService _collisorService;
        public GameState State => _gameState.State;

        public GameService(
            GameStateService gameStateService, 
            CollisorService collisorService
        )
        {
            _gameState = gameStateService;
            _collisorService = collisorService;
            _collisorService.OnCollision += OnCollision;
        }

        public void Update()
        {
            _gameState.State.Snakes.ForEach(snake => snake.Update());

            var collidables = new List<ICollidable>();

            if (!_gameState.State.Snakes.Any()) return;

            collidables.AddRange(_gameState.State.Snakes);
            collidables.AddRange(_gameState.State.Fruits);

            _collisorService.setCollidables(collidables).Check();
        }

        private void OnCollision(object collisorService, CollisionEventArgs args)
        {
            var collidables = args.Collidables;
            var fruit = collidables.FirstOrDefault(c => c.GetType() == typeof(Fruit));

            if (fruit != null)
            {
                var snake = collidables.FirstOrDefault(c => c.GetType() == typeof(Snake));

                _gameState.PointTo(snake?.Id);
                _gameState.RemoveFruit(fruit?.Id);
                _gameState.GenerateFruit();
                return;
            }

            var collided1 = CollidedToOponent(collidables[0], collidables[1]);
            var collided2 = CollidedToOponent(collidables[1], collidables[0]);

            if (collided1)
            {
                _gameState.RemoveSnake(collidables[0].Id);
            }
            
            if (collided2)
            {
                _gameState.RemoveSnake(collidables[1].Id);
            }
        }

        private bool CollidedToOponent(ICollidable collidable1, ICollidable collidable2)
        {
            var hitbox1 = collidable1.Hitboxes.First();

            var collided = from hitbox2 in collidable2.Hitboxes
                where (
                    hitbox1.X >= hitbox2.X
                    && hitbox1.X + hitbox1.Width <= hitbox2.X + hitbox2.Width
                    && hitbox1.Y >= hitbox2.Y

                    && hitbox1.Y + hitbox1.Height <= hitbox2.Y + hitbox1.Height
                )
                select hitbox2;

            return collided.Any();
        }
    }
}