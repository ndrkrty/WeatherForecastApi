using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApi.Hubs
{
    public class ServerHub : Hub
    {
        public async Task SendMessageAsync(string elapsedTime, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", elapsedTime, message);
        }
    }
}
