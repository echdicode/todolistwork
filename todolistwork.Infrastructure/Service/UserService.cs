using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Application.ICache;
using todolistwork.Application.IService;
using todolistwork.Application.Repository;
using todolistwork.Core.Entities;
using todolistwork.Core.Models;
using todolistwork.Core.Unit;
using todolistwork.Infrastructure.database.Redis;
using static Dapper.SqlMapper;

namespace todolistwork.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService( IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<User> Login(User entity)
        {
            try
            {


                var user = await _unitOfWork.UserRepository.LoginUser(entity);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserService Login: ", ex);

                return null;
            }
        }


        public async Task<string> Registration(User entity)
        {
            try
            {
                var user = User.Create(entity.Email, entity.Password, entity.UserName);

                var result = await _unitOfWork.UserRepository.AddAsync(user);



                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserService Registration:  ", ex);
                return null;
            }
        }

        public async Task<User> AddAsync(User entity)
        {
            var data = await _unitOfWork.UserRepository.AddAsync(entity);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(entity.Id);

            return user;
        }

    

        public async Task<string> ChangePassword(User entity)
        {
            var data = await _unitOfWork.UserRepository.UpdatePassword(entity);
            return data;

        }

        public async Task<User> ChangeProfile(User entity)
        {
            var  data = await _unitOfWork.UserRepository.UpdateAsyncByUser(entity);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(entity.Id);

            return user;

        }

        public async Task<string> DeleteAsync(string id)
        {
            var data = await _unitOfWork.UserRepository.DeleteAsync(id);
            return data;

        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var data = await _unitOfWork.UserRepository.GetAllAsync();
            return data;

        }

        public async Task<User> GetProfile(string Id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(Id);
            return user;

        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(email);
            return user;

        }



        public async Task<string> ResetPassword(User entity)
        {
            var data = await _unitOfWork.UserRepository.UpdatePassword(entity);
            return data;
        }

        public Task<User> Signin(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
