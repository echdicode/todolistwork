using todolistwork.Core.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySqlConnector;
using todolistwork.Infrastructure.database.Mysql;
using todolistwork.Application.Repository;

namespace todolistwork.Infrastructure.Repository
{
    public class TaskUserRepository : ITaskUserRepository
    {
        private readonly IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        public TaskUserRepository(IDbTransaction transaction)
        {
            _transaction=transaction;
        }
        public async Task<string> AddAsync(TaskUser entity)
        {
         
                var result = await _connection.ExecuteAsync(TaskUserQueries.AddTaskUser, entity);
                return result.ToString();
            
        }

        public async Task<string> DeleteAsync(long id)
        {
           
                var result = await _connection.ExecuteAsync(TaskUserQueries.DeleteTaskUser, new { Id = id });
                
                return result.ToString();

            
        }

        public async Task<IReadOnlyList<TaskUser>> GetAllAsync()
        {
          
                var result = await _connection.QueryAsync<TaskUser>(TaskUserQueries.AllTaskUser);
                return result.ToList();
        
        }

        public async Task<TaskUser> GetByIdAsync(long id)
        {
            
                var result = await _connection.QuerySingleOrDefaultAsync<TaskUser>(TaskUserQueries.TaskUserById, new { Id = id });
                return result;
            
        }

        public async Task<IReadOnlyList<TaskUser>> GetTaskUserByNeedToDo()
        {
          
                var result = await _connection.QueryAsync<TaskUser>(TaskUserQueries.TaskUserByNeedToDo);
                return result.ToList();
            
        }

        public async Task<string> UpdateAsync(TaskUser entity)
        {
            
                var result = await _connection.ExecuteAsync(TaskUserQueries.UpdateTaskUser, entity);
                return result.ToString();
            
        }

     
    }
}
