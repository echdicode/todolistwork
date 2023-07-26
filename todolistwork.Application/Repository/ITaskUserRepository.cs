using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Core.Entities;

namespace todolistwork.Application.Repository
{
    public interface ITaskUserRepository : IRepository<TaskUser>
    {
        Task<IReadOnlyList<TaskUser>> GetTaskUserByNeedToDo();
    }
}
