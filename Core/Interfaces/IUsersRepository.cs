using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<User> GetUserByEmail(string email);
        void Add(User user);
        void Delete(User user);
    }
}
