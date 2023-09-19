using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Core.Entities;

namespace todolistwork.Application.IService
{
    public interface ITaskUserService
    {
        Task<IReadOnlyList<TaskUser>> GetAllTaskUser(string userId);
        Task<TaskUser> GetTaskUserById(string id, string userId);
        Task<TaskUser> AddTaskUser(TaskUser entity);
        Task<TaskUser> UpdateTaskUser(TaskUser entity);
        Task<string> DeleteTaskUser(string id, string userId);
        Task<IReadOnlyList<TaskUser>> GetTaskUserByNeedToDo(string userId);
    }
}
