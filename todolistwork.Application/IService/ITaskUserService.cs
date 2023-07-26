using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Core.Entities;

namespace todolistwork.Application.Service.IService
{
    public interface ITaskUserService
    {
        Task<IReadOnlyList<TaskUser>> GetAllTaskUser();
        Task<TaskUser> GetTaskUserById(long id);
        Task<TaskUser> AddTaskUser(TaskUser entity);
        Task<TaskUser> UpdateTaskUser(TaskUser entity);
        Task<string> DeleteTaskUser(long id);
        Task<IReadOnlyList<TaskUser>> GetTaskUserByNeedToDo();
    }
}
