using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SnakeGameBackend.Hubs
{
    public class GameHub : Hub
    {

        public GameHub()
        {
            var timer = new Timer(
                callback: new TimerCallback(SendMessage)
                
            );
        }
        
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage", "ahhhhhhhhhhhhhhhh");
        }

        public async Task Move(string key)
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;


            await Clients.All.SendAsync("MoveTo", "move"+textInfo.ToTitleCase(key));
        }
        
    }
}
