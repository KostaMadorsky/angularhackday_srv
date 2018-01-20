using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SirnalRServer.Controllers
{
    public class Message
    {
        public string text { get; set; }
        public string author { get; set; }
    }

    public class Input
    {
        public string msg { get; set; }
        public string author { get; set; }
        public string room { get; set; }
    }

    [Route("api/messages")]
    [Produces("application/json")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> ctx;

        public ChatController(IHubContext<ChatHub> ctx)
        {
            this.ctx = ctx;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Input input)
        {
            await ctx.Clients.Group(input.room).InvokeAsync("messageReceived", new Message {
                text = input.msg,
                author = input.author
            });

            return Ok();
        }

        
    }
}