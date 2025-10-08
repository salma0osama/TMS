using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.core.Entities;

namespace TMS.app.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetUser(string username, string password);
        Task<bool> UserExists(string username);
    }
}
