using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalR_App_Api.Hubs
{
    public class MyHub:Hub
    {
        public static List<string> Names { get; set; } = new List<string>();
        private static int ClientCount { get; set; } = 0;

        public async Task SendName(string name)   //Klavyeden girdiğimiz isimler
        {
            Names.Add(name);
            await Clients.All.SendAsync("ReceiveName", name);
        }
        public async Task GetNames()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }
        public override async Task OnConnectedAsync()
        {
            ClientCount++;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            ClientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", ClientCount);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
