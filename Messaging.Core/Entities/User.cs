using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Messaging.Core.Entities
{
    public class User
    {

        [BsonId]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<Message> Messages {get;set;}
        public IEnumerable<BlockList> BlockUserList {get;set;}

    }
}
