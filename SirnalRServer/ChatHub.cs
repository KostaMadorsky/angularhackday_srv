using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SirnalRServer
{
    public class ChatHub: Hub
    {
        public async Task JoinRoom(string room)
        {
            await Groups.AddAsync(this.Context.ConnectionId, room);
        }

        public async Task Leave(string room)
        {
            await Groups.RemoveAsync(this.Context.ConnectionId, room);
        }
    }

}
