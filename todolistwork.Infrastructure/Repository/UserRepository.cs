using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using todolistwork.Application.Repository;
using todolistwork.Core.Entities;
using todolistwork.Core.Models;
using todolistwork.Infrastructure.database.Mysql;

using static Dapper.SqlMapper;
namespace todolistwork.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;

        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
       

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    var results = await connection.QueryAsync<User>(UserQueries.AllUser);
                    return results.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<User> GetByIdAsync(string id)
        {
            using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var results = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.UserById, new { Id = id });
                return results;
            }
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var results = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.UserByEmail, new { Email = email });
                return results;
            }
        }

        public async Task<string> AddAsync(User entity)
        {
            using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(UserQueries.AddUser, entity);
                return result.ToString();
            }
        }

        public async Task<string> UpdateAsyncByUser(User entity)
        {

            using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(UserQueries.UpdateUser, entity);
                return result.ToString();
            }
        }

        public async Task<string> DeleteAsync(string id)
        {
            using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(UserQueries.DeleteUser, new { Id = id  });
                return result.ToString();
            }
        }

   

        public async Task<User> LoginUser(User entity)
        {
            using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var results = await connection.QuerySingleOrDefaultAsync<User>(UserQueries.UserLogin, entity);
                return results;
            }
        }

        public async Task<string> UpdatePassword(User entity)
        {
            using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(UserQueries.UpdatePassword, entity);
                return result.ToString();
            }
        }
    }
}
