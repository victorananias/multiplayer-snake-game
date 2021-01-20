using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MultiplayerSnakeGame.Entities;
using MultiplayerSnakeGame.Services;

namespace MultiplayerSnakeGame.Hubs
{
    public class GameHub : Hub
    {
        private GamesService _gamesService;
        private ActionsService _actionsService;

        public GameHub(GamesService gamesService, ActionsService actionsService)
        {
            _gamesService = gamesService;
            _actionsService = actionsService;
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            _gamesService.DisconnectPlayer(Context.ConnectionId);
        }

        public void JoinGame(string gameId)
        {
            if (string.IsNullOrWhiteSpace(gameId))
            {
                return;
            }
            
            _gamesService.ConnectPlayer(gameId, Context.ConnectionId);
        }

        public void StartAction(string action)
        {
            _actionsService.Start(Context.ConnectionId, action);
        }

        public void StopAction(string action)
        {
            _actionsService.Stop(Context.ConnectionId, action);
        }
    }
}
