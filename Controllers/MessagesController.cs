using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosApiJwt.Interfaces;
using PosApiJwt.Models;

namespace PosApiJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized("Invalid user");
            }

            var messages = await messageService.ReadMessages(claim.Value);
            if (messages == null || !messages.Any())
            {
                return BadRequest($"No message was found");
            }
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized(ServerResponse("xxxxx", "Invalid user or old token!"));
            }

            var message = await messageService.ReadMessage(claim.Value, id);
            if (message == null)
            {
                return BadRequest(ServerResponse(claim.Value, $"Message {id} was not found"));
            }
            return Ok(message);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized(ServerResponse("xxxxx", "Invalid user or old token!"));
            }
            if (claim.Value != message.From)
            {
                return Unauthorized(ServerResponse(claim.Value, "Phishing attempt!"));
            }
            message = await messageService.CreateMessage(message);
            if (message == null)
            {
                return BadRequest(ServerResponse(claim.Value, "Unsaved message!"));
            }
            return Ok(message);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "OnlyNonBlockedUser")]
        public async Task<ActionResult<Message>> PutMessage(int id, Message message)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized(ServerResponse("xxxxx", "Invalid user or old token!"));
            }
            if (claim.Value != message.From)
            {
                return Unauthorized(ServerResponse(claim.Value, "Phishing attempt!"));
            }
            if (id != message.MessageId)
            {
                return Unauthorized(ServerResponse(claim.Value, "Ids do not match!"));
            }
            message = await messageService.UpdateMessage(message);
            if (message == null)
            {
                return BadRequest(ServerResponse(claim.Value, "Message has not been changed!"));
            }
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized();
            }

            var message = await messageService.ReadMessage(claim.Value, id);
            if (message == null)
            {
                return NotFound();
            }

            await messageService.DeleteMessage(id);
            return NoContent();
        }

        private Message ServerResponse(string to, string msg)
        {
            return new Message
            {
                To = to,
                From = "server",
                MsgBody = new MsgBody { Msg = msg, Stamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") }
            };
        }
    }
}