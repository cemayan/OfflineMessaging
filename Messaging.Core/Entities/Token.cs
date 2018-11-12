using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Messaging.Core.Entities
{
    public class Token
    {

        public string AuthToken {get;set;}

    }
}
