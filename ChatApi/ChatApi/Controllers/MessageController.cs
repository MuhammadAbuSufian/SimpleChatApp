using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        private readonly IMessageService _service;
        public MessageController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllOrderByMessage());
        }

        [HttpGet("received-messages/{userId}")]
        public async Task<IActionResult> GetUserReceivedMessages(string userId)
        {
            return Ok(await _service.GetMessagesByUserId(userId));
        }
    }
}
