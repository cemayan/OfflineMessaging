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

    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
    
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthController> _logger;
        

        public AuthController( IConfiguration configuration, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _authRepository = new AuthRepository();
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetToken([FromBody]User login)
        {
            var user = CheckUser(login);

            if (user != null)
            {
                var tokenString = CreateToken(user);
                //_logger.LogInformation(user.Username + " logged in");
                return Ok(new { token = tokenString, result = "success" });
            }
             _logger.LogError("Unauthorized login");
            return Unauthorized();
        }

        public string Deneme(){
           
            return "mf";
        }

        public string DbDeneme(){
           
            return  _authRepository.CheckDB();
        }

        private string CreateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire_time = Double.Parse(_configuration["JwtExpireMinute"]);
            var token = new JwtSecurityToken(_configuration["JwtIssuer"],
              _configuration["JwtIssuer"],
              expires: DateTime.Now.AddMinutes(expire_time),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private User CheckUser(User login)
        {
            var user = _authRepository.Login(login);
           if(user!=null){
                HttpContext.Session.SetString("user_id", user._id.ToString());
                Program.user_id = user._id.ToString();
                return user;
           }
           else{
                 //_logger.LogInformation("User not  found");
                 return null;
           }
    
        }


        [HttpPost]
        public IActionResult  ValidateToken([FromBody]Token token)
        {

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var validationParameters = new TokenValidationParameters
        {
            ClockSkew = TimeSpan.FromMinutes(5),
            IssuerSigningKey = creds.Key,
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidAudience = _configuration["JwtIssuer"],
            ValidateIssuer = true,
            ValidIssuer = _configuration["JwtIssuer"]
        };

        try
        {
            var claimsPrincipal = new JwtSecurityTokenHandler()
                .ValidateToken(token.AuthToken, validationParameters, out var rawValidatedToken);


            return Ok(new { result = "success"});
        }
        catch (SecurityTokenValidationException stvex)
        {
             _logger.LogInformation("wrong token");
             return StatusCode(500);
        }
        catch (ArgumentException argex)
        {

            return StatusCode(400);
        }
            }

        }
}
