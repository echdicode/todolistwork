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
        private readonly IConfiguration configuration;

        public TaskUserRepository(IConfiguration configuration)
        {
           this.configuration = configuration;  
        }

       

        public async Task<IReadOnlyList<TaskUser>> GetAllAsync(string userId)
            
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    var results = await connection.QueryAsync<TaskUser>(TaskUserQueries.AllTaskUser, new { UserId = userId });
                    return results.ToList();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("GetAllAsync:  ");
                Console.WriteLine(ex.Message);
                return null;
            }
        
        }

        public async Task<TaskUser> GetByIdAsync(string id )
        {
        
            try
            {
                using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    Console.WriteLine("GetByIdAsync: "+id );

                    var result = await connection.QuerySingleOrDefaultAsync<TaskUser>(TaskUserQueries.TaskUserById, new {Id= id});
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetByIdAsync:  ", ex);
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<string> AddAsync(TaskUser entity)
        {
            try {
                using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    Console.WriteLine("TaskUserRepository AddAsync:    "+ entity.Id);

                    var result = await connection.ExecuteAsync(TaskUserQueries.AddTaskUser, entity);
                    return result.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TaskUserRepository AddAsync", ex);
                Console.WriteLine(ex.Message);
                return "sai";
            }
        }

        public async Task<string> DeleteAsync(string id)
        {
            try {
                using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(TaskUserQueries.DeleteTaskUser, new { Id = id });
                    return result.ToString();
                }
            }
            catch(Exception ex) { 
                
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<IReadOnlyList<TaskUser>> GetTaskUserByNeedToDo(string userId)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    var results = await connection.QueryAsync<TaskUser>(TaskUserQueries.TaskUserByNeedToDo, new { UserId = userId });
                    return results.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<string> UpdateAsync(TaskUser entity)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(configuration.GetConnectionString("DBConnection")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(TaskUserQueries.UpdateTaskUser, entity);
                    Console.WriteLine(result);

                    return result.ToString();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
