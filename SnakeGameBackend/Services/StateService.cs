using System.Collections.Generic;
using SnakeGameBackend.Entities;

namespace SnakeGameBackend.Services
{
    public class StateService
    {
        private GameState _state;

        public StateService()
        {
            _state = new GameState
            {
                snakes = new List<Snake>()
                {
                    new Snake()
                    {
                        head = new SnakePiece() {x = 100, y = 100}
                    }                },
                fruit = new Fruit() {x = 200, y = 200}
            };
        }
    }
}