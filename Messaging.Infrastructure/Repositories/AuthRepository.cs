using System;
using Microsoft.Extensions;
using MongoDB.Driver;
using Messaging.Infrastructure.DbContext;
using Messaging.Core.Entities;
using Microsoft.AspNetCore.Http;
using Messaging.Core.Interfaces;

namespace Messaging.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MessagingContext _context = null;

        public AuthRepository()
        {
            _context = new MessagingContext();
        }

        public String CheckDB(){

            if(_context == null)
            {
                return "Something went wrong";
            }
            else{
                return "OK!";
            }
        }

        public bool DeleteUser(User model)
        {
            throw new NotImplementedException();
        }


        public bool NewUser(User model)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User model)
        {
            throw new NotImplementedException();
        } 
        public User Login(User model)
        {
           var user =  _context.Users.Find(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();
            var aa= _context.Users.Find(_ => true).ToList();

            if(user!=null){
                return user;
            }
            return null;

        }
        
    }
}
