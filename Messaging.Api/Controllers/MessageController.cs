using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messaging.Core.Entities;
using Messaging.Infrastructure.Repositories;
using Messaging.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;

namespace Messaging.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : Controller
    {

        private readonly IMessageRepository _messageRepository;
        private readonly IConfiguration _configuration;

        private readonly String user_id;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IConfiguration configuration,  ILogger<MessageController> logger)
        {
            _configuration = configuration;
            _messageRepository = new MessageRepository();
            user_id =  Program.user_id ;
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return _messageRepository.GetMessages(user_id);
        }

        [HttpGet("{id}")]
        public Message Get(string id)
        {
            var message = _messageRepository.GetMessage(user_id,id);
            if( message!=null){
                return message;
            }
            else{
                 _logger.LogInformation("message couldn't find");
                return  null;
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpPost]
        public IActionResult Post([FromBody]Message message)
        {
            if( ModelState.IsValid &&  _messageRepository.SendMessage(user_id,message)){
                return Ok();
            }
            else{
                 _logger.LogInformation("when messege sending, something went wrong");
                return  StatusCode(500);
            }
        }

    }
}
