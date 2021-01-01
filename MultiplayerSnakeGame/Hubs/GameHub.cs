using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MultiplayerSnakeGame.Services;

namespace MultiplayerSnakeGame.Hubs
{
    public class GameHub : Hub
    {
        private GamesService _gamesService;
        private KeyboardService _keyboardService;

        public GameHub(GamesService gamesService, KeyboardService keyboardService)
        {
            _gamesService = gamesService;
            _keyboardService = keyboardService;
        }

        public void JoinGame(string gameId)
        {
            if (string.IsNullOrWhiteSpace(gameId))
            {
                return;
            }
            
            _gamesService.AddSnake(gameId, Context.ConnectionId);
            
            Groups.AddToGroupAsync(Context.ConnectionId, groupName: gameId);
        }

        public void KeyPressed(string key)
        {
            _keyboardService.Press(Context.ConnectionId, key);
        }

        public void KeyReleased(string key)
        {
            _keyboardService.Release(Context.ConnectionId, key);
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            _gamesService.DisconnectPlayer(Context.ConnectionId);
        }
    }
}
