using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Messaging.Core.Entities;
using Messaging.Infrastructure.Repositories;
using Messaging.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;

namespace Messaging.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserBlockController : Controller
    {
    
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserBlockController> _logger;
        private readonly String user_id;
        

        public UserBlockController( IConfiguration configuration, ILogger<UserBlockController> logger)
        {
            _configuration = configuration;
            _userRepository = new UserReposityory();
            _logger = logger;
            user_id = Program.user_id ;
        }

        [HttpPost]
        public IActionResult Post([FromBody]String block_user_id)
        {
           if( block_user_id!=null  && _userRepository.BlockUser(block_user_id,user_id)){
                return Ok();
            }
            else{
                _logger.LogInformation("User couldn't block");
                return  StatusCode(500);
            }
        }
    }
}
