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
using Messaging.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using Messaging.Core.Interfaces;

namespace Messaging.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
    
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly String user_id;
        

        public UserController( IConfiguration configuration, ILogger<UserController> logger)
        {
            _configuration = configuration;
            _userRepository = new UserReposityory();
            _logger = logger;
            user_id = Program.user_id ;
        }


        [HttpPost]        
        public IActionResult GetUser([FromBody]User user)
        {
           var user_ =_userRepository.GetUser(user.Username);
     
           if( user!=null && user!=null){
                return Ok(new {user = user_});
            }
            else{
                 _logger.LogInformation("User couldn't get");
                return  StatusCode(500);
            }
        }
    }
}
