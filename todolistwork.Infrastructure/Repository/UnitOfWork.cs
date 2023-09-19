using Microsoft.Extensions.Configuration;
using MySqlConnector;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolistwork.Application.Repository;

namespace todolistwork.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        /*  public UnitOfWork(ITaskUserRepository TaskUsers, IUserRepository UserRepository, IAdminUserRepository AdminUsers)
          {
              this.TaskUsers = TaskUsers;
              this.UserRepository = UserRepository;
              this.AdminUsers = AdminUsers;
          }*/
        public UnitOfWork(ITaskUserRepository TaskUsers, IUserRepository UserRepository)
        {
            this.TaskUsers = TaskUsers;
            this.UserRepository = UserRepository;
   
        }


        public ITaskUserRepository TaskUsers { get; set; }

        public IUserRepository UserRepository { get; set; }   
    }
}
