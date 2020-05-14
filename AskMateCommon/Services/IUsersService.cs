using AskMate.Common.Domain;
using System.Collections.Generic;

namespace AskMate.Common.Services
{
    public interface IUsersService
    {
        User GetOne(int id);
        List<User> GetAll();
        User Login(string username, string password);
        void Register(string username, string password);
    }
}
