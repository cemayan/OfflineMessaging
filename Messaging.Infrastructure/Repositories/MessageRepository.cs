using System;
using System.Collections.Generic;
using Microsoft.Extensions;
using MongoDB.Driver;
using Messaging.Infrastructure.DbContext;
using Messaging.Core.Entities;
using MongoDB.Bson;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Messaging.Core.Interfaces;

namespace Messaging.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MessagingContext _context = null;

        public MessageRepository()
        {
            _context = new MessagingContext();
        }

        public IEnumerable<Message> GetMessages(String userid)
        {
             ObjectId user_id = ObjectId.Parse(userid);
            return  _context.Users.Find(x=>x._id == user_id).FirstOrDefault().Messages;
        }
         
        public Message GetMessage(String userid, String id)
        {
            try 
            {
                ObjectId user_id = ObjectId.Parse(userid);
                var user =  _context.Users.Find(x=>x._id == user_id).FirstOrDefault();
                var t = JsonConvert.DeserializeObject<List<Message>>(user.Messages.ToJson());
                var message = t.Find(x=>x._id ==  id);
                return message;
            }
            catch(Exception e)
            {
                return null;
            }
        }


        public bool SendMessage(String user_id, Message message)
        {        
            try
            {
                if(!CheckBlockUser(user_id,message))
                {
                    var filter = Builders<User>.Filter.Eq(s => s._id, ObjectId.Parse(user_id));
                    var user = _context.Users.Find(x=>x._id == ObjectId.Parse(user_id)).FirstOrDefault();

                    var t = JsonConvert.DeserializeObject<List<Message>>(user.Messages.ToJson());
                    t.Insert(0,message);
                    user.Messages = t;
                    _context.Users.ReplaceOne(filter, user);
            
            
                    return true;
                }
                else
                {
                    return false;
                }
        
            }
            catch(Exception e){
                 return false;
            }
          
        }



        public bool CheckBlockUser(String user_id,Message message)
        {
            var user =  _context.Users.Find(x=>x._id == ObjectId.Parse(user_id)).FirstOrDefault();
            var t = JsonConvert.DeserializeObject<List<BlockList>>(user.BlockUserList.ToJson());

            if(t.IndexOf(new BlockList {_id=message.user_id})>0){
                  return true;
            }
            else{
                return false;
            }
        }
    }
}
