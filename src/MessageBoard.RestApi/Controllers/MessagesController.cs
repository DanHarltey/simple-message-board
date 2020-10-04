using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MessageBoard.Contracts;
using MessageBoard.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessageBoard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessages _messageThreads;

        public MessagesController(ILogger<MessagesController> logger, IMessages messageThreads)
        {
            _logger = logger;
            _messageThreads = messageThreads;
        }

        [HttpGet]
        public Task<IEnumerable<string>> Get() =>  _messageThreads.Get();

        [HttpPost]
        public Task Post(Message message) => _messageThreads.AddMessage(message.Text);
    }
}
