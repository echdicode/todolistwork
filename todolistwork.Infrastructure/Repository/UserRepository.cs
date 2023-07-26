using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Application.Repository;
using todolistwork.Core.Entities;
using todolistwork.Infrastructure.database.Mysql;

namespace todolistwork.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        public UserRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }
        public async Task<string> AddAsync(User entity)
        {
            var result = await _connection.ExecuteAsync(UserQueries.AddUser, entity);
            return result.ToString();
        }

        public Task<string> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
