using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SirnalRServer.Controllers
{
    

    [Route("api/messages")]
    [Produces("application/json")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _ctx;

        public ChatController(IHubContext<ChatHub> ctx)
        {
            this._ctx = ctx;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]HubMessage input)
        {
            await _ctx.Clients.Group(input.author.room).InvokeAsync("onMessageReceived", input);

            return Ok();
        }

        
    }
}