using System.Collections.Generic;
using MultiplayerSnakeGame.Data;

namespace MultiplayerSnakeGame.Services
{
    public class ActionsService
    {
        private GamesService _gamesService;

        public ActionsService(GamesService gamesService)
        {
            _gamesService = gamesService;
        }

        public void Start(string playerId, string action)
        {
            if (MoveKeys.All().Contains(action))
            {
                _gamesService.MoveOrBoost(playerId, action);
            }
        }

        public void Stop(string playerId, string action)
        {
            if (MoveKeys.All().Contains(action))
            {
                _gamesService.StopSnakeBoost(playerId);
            }
        }
    }
}