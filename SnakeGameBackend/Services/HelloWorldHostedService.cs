using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SnakeGameBackend.Hubs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGameBackend.Services
{
    public class HelloWorldHostedService : BackgroundService
    {
        private IHubContext<GameHub> _hubContext;

        public HelloWorldHostedService(IHubContext<GameHub> hubContext)
        {
            _hubContext = hubContext;       
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var clients = _hubContext.Clients;
            
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    await clients.All.SendAsync("ReceiveMessage", "ahhhhhhhhhhhhhhhh");

            //    await Task.Delay(1000/60);
            //}

            Console.WriteLine("Finished");
        }
    }
}
