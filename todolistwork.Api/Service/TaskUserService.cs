using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Application.ICache;
using todolistwork.Application.Repository;
using todolistwork.Application.Service.IService;
using todolistwork.Core.Entities;

namespace todolistwork.Application.Service
{
    public class TaskUserService : ITaskUserService
    {
        private readonly IRedisService _redisService;
        private readonly IUnitOfWork _unitOfWork;

        public TaskUserService(IUnitOfWork _unitOfWork, IRedisService _redisService) { 
            this._unitOfWork = _unitOfWork;
            this._redisService = _redisService;

        }

        public Task<TaskUser> AddTaskUser(TaskUser entity)
        {
            throw new NotImplementedException();

        }

        public Task<string> DeleteTaskUser(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TaskUser>> GetAllTaskUser()
        {
            throw new NotImplementedException();
        }

        public Task<TaskUser> GetTaskUserById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TaskUser>> GetTaskUserByNeedToDo()
        {
            throw new NotImplementedException();
        }

        public Task<TaskUser> UpdateTaskUser(TaskUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
