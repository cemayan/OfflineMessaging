using System;
using Messaging.Core.Entities;

namespace Messaging.Core.Interfaces
{
    public interface IAuthRepository
    {
        User  Login(User model);
        Boolean NewUser(User model);
        Boolean UpdateUser(User model);
        Boolean DeleteUser(User model);
        String CheckDB();
    }
}
