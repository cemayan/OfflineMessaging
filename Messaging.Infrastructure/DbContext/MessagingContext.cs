using System;
using Microsoft.Extensions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Messaging.Core.Entities;

namespace Messaging.Infrastructure.DbContext
{

    public class MessagingContext
    {

        private readonly IMongoDatabase _database = null;

        public MessagingContext(){
        
            var client = new MongoClient("mongodb://mongo:27017");
            if (client != null)
      
                _database = client.GetDatabase("messaging");

        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("users");
            }
        }

    }
}
