using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SirnalRServer.Controllers;

namespace SirnalRServer
{
    public class HubMessage
    {
        public string text { get; set; }
        public HubUser author { get; set; }
        public DateTime dateSent { get; set; }
    }

    public class HubUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string room { get; set; }
    }


    public class ChatHub: Hub
    {
        const string onMessageReceivedEvent = "onMessageReceived";

        public async Task JoinRoom(string room)
        {
            await Groups.AddAsync(this.Context.ConnectionId, room);
        }

        public async Task Leave(string room)
        {
            await Groups.RemoveAsync(this.Context.ConnectionId, room);
        }

        public async Task SendMessage(HubMessage msg)
        {
            msg.author.id = this.Context.ConnectionId;
            await Clients.Group(msg.author.room).InvokeAsync(onMessageReceivedEvent, msg);
        }
    }

}
