using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messaging.Core.Entities;

namespace Messaging.Core.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(String username);
        User GetUser(String username);
         Boolean BlockUser(String user_id, string block_user_id);
    }
}
