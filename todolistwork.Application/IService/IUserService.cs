using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Core.Entities;
using todolistwork.Core.Models;

namespace todolistwork.Application.IService
{
    public interface IUserService
    {
        Task<User> Login(User entity);
        Task<User> Signin(User entity);
        Task<string> Registration(User entity);
        Task<string> ChangePassword(User entity);
        Task<string> ResetPassword(User entity);
        Task<User> GetProfile(string Id);
        Task<User> ChangeProfile(User entity);
        Task<User> GetUserByEmail(string email);

        //admin
        Task<IReadOnlyList<User>> GetAllAsync();
    
        Task<User> AddAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<string> DeleteAsync(string id);

    }
}
