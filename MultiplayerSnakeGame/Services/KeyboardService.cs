namespace MultiplayerSnakeGame.Services
{
    public class KeyboardService
    {
        private GamesService _gamesService;

        public KeyboardService(GamesService gamesService)
        {
            _gamesService = gamesService;
        }

        public void Press(string playerId, string key)
        {
            var direction = key switch
            {
                "d" => "right",
                "a" => "left",
                "w" => "up",
                "s" => "down",
                _ => ""
            };

            if (string.IsNullOrEmpty(direction)) 
            {
                return;
            }
            
            _gamesService.MoveOrBoost(playerId, direction);
        }

        public void Release(string playerId, string key)
        {
            _gamesService.StopSnakeBoost(playerId);
        }
    }
}