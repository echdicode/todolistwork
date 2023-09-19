using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Core.Entities;
using todolistwork.Core.Models;

namespace todolistwork.Application.Repository
{
    public interface IUserRepository 
    {
      
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<User> LoginUser(User entity);

        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string  email);
        Task<string> AddAsync(User entity);
        Task<string> UpdateAsyncByUser(User entity);
        Task<string> UpdatePassword(User entity);

        Task<string> DeleteAsync(string id);

    }
}
