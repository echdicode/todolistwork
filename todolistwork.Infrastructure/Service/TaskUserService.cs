using todolistwork.Application.ICache;
using todolistwork.Application.Repository;
using todolistwork.Application.IService;
using todolistwork.Core.Entities;
using System.Collections.Generic;
using static Dapper.SqlMapper;

namespace todolistwork.Application.Service
{
    public class TaskUserService : ITaskUserService
    {

        public static string KeyRedisTaskUser ="TaskUser:{0}";
        private readonly IRedisService _redisService;
        private readonly IUnitOfWork _unitOfWork;

        public TaskUserService(IUnitOfWork _unitOfWork, IRedisService _redisService) { 
            this._unitOfWork = _unitOfWork;
            this._redisService = _redisService;

        }

        public async Task<TaskUser> AddTaskUser(TaskUser entity)
        {
            try {
                string key = string.Format(KeyRedisTaskUser, entity.UserId);
                var result = await _unitOfWork.TaskUsers.AddAsync(entity);
                var data = await _unitOfWork.TaskUsers.GetByIdAsync(entity.Id);
              var result1 = await _redisService.SetDataHash(key, entity.Id, entity);
                Console.WriteLine("AddTaskUser111111:");

                Console.WriteLine(data);
                return data;
            }
            catch(Exception ex) {
                Console.WriteLine("AddTaskUser:  ");

                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<string> DeleteTaskUser(string id,string userId)
        {
            try {
                string key = string.Format(KeyRedisTaskUser, userId);

                var result = await _redisService.DeleteDataHash(key, id);
                var data= await _unitOfWork.TaskUsers.DeleteAsync(id);
                return data;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<IReadOnlyList<TaskUser>> GetAllTaskUser(string userId )
        {
            try
            {
                var redisData = await _redisService.GetAllDataHash<TaskUser>(KeyRedisTaskUser);
                if (redisData.Count()<=0)
                {
                    string key = string.Format(KeyRedisTaskUser, userId);
                    var data = await _unitOfWork.TaskUsers.GetAllAsync(userId);
                    foreach (TaskUser result in data)
                    {
                        var result1 = await _redisService.SetDataHash(key, result.Id, result);

                    }
                    return data;
                }
                return redisData;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<TaskUser> GetTaskUserById(string id, string userId)
        {
            try
            {
                var redisData = await _redisService.GetDataByIdHash<TaskUser>(KeyRedisTaskUser, id);
                if (redisData == null)
                {
                    string key = string.Format(KeyRedisTaskUser, userId);

                    var data = await _unitOfWork.TaskUsers.GetByIdAsync(id);
                    var isset = await _redisService.SetDataHash(key, data.Id, data);
                    return data;
                }
                return redisData;
           /*     var data = await _unitOfWork.TaskUsers.GetByIdAsync(entity);
                return data;*/
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetTaskUserById: ");

                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<IReadOnlyList<TaskUser>> GetTaskUserByNeedToDo(string  userId)
        {
            try
            {
                var data = await _unitOfWork.TaskUsers.GetTaskUserByNeedToDo(userId);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<TaskUser> UpdateTaskUser(TaskUser entity)
        {
            try
            {
                string key = string.Format(KeyRedisTaskUser, entity.UserId);

                var data = await _unitOfWork.TaskUsers.UpdateAsync(entity);
                var result = await _unitOfWork.TaskUsers.GetByIdAsync(entity.Id);
                var isset = await _redisService.SetDataHash<TaskUser>(key, entity.Id, entity);
                
                return result;
            
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
