using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messaging.Core.Entities;


namespace Messaging.Core.Interfaces
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetMessages(String userid);

        Message GetMessage(String userid,String id);

        bool SendMessage(String userid, Message message);
    }
}
