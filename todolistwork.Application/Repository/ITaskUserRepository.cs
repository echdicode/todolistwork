using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Core.Entities;

namespace todolistwork.Application.Repository
{
    public interface ITaskUserRepository 
    {
        Task<IReadOnlyList<TaskUser>> GetTaskUserByNeedToDo(string userId);
        Task<IReadOnlyList<TaskUser>> GetAllAsync(string userId);
        Task<TaskUser> GetByIdAsync(string id);
        Task<string> AddAsync(TaskUser entity);
        Task<string> UpdateAsync(TaskUser entity);
        Task<string> DeleteAsync(string id);
    }
}
