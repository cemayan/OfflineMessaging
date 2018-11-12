using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Messaging.Core.Entities
{
    public class Message
    {
        public String _id { get; set; }
        public string Text { get; set; }
        public string user_id { get; set; }

    }
}
