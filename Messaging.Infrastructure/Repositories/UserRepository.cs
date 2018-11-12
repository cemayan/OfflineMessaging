using System;
using System.Collections.Generic;
using Microsoft.Extensions;
using MongoDB.Driver;
using Messaging.Infrastructure.DbContext;
using Messaging.Core.Entities;
using MongoDB.Bson;
using Newtonsoft.Json;
using Messaging.Core.Interfaces;

namespace Messaging.Infrastructure.Repositories
{
    public class UserReposityory : IUserRepository
    {
        private readonly MessagingContext _context = null;

        public UserReposityory()
        {
            _context = new MessagingContext();
        }

        public bool BlockUser(string user_id, string block_user_id)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(s => s._id, ObjectId.Parse(user_id));
                var user = _context.Users.Find(x=>x._id == ObjectId.Parse(user_id)).FirstOrDefault();
                var t = JsonConvert.DeserializeObject<List<BlockList>>(user.BlockUserList.ToJson());
                t.Insert(0,new BlockList{_id = block_user_id});
                user.BlockUserList = t;
                _context.Users.ReplaceOne(filter, user);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        
        }

        public User GetUser(String username)
        {
            return  _context.Users.Find(x=>x.Username ==username).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers(string username)
        {
            return _context.Users.Find(x=>x.Username.Contains(username)).ToEnumerable();
        }

        User IUserRepository.GetUser(string username)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IUserRepository.GetUsers(string username)
        {
            throw new NotImplementedException();
        }
    }
}
