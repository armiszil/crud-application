using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace DE6ZVJ_ADT_2022_23_1.Endpoint.Services
{
    public class SignalerHub:Hub
    {

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Caller.SendAsync("Disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
