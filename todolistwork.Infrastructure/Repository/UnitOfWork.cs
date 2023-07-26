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
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        private readonly IConfiguration configuration;


        public IContactRepository Contacts { get; set; }

        public IAdminUserRepository AdminUsers { get; set; }


        public ITaskUserRepository TaskUsers { get; set; }


        public IUserAccountsRepository UserAccounts { get; set; }

        public IUserRepository UserAccountsRepository { get; set; }
        public UnitOfWork(IConfiguration configuration)
        {
            this.configuration = configuration;
            _connection = new MySqlConnection(configuration.GetConnectionString("DBConnection"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        public void Dispose()
        {
            _transaction.Dispose();
            _connection.Dispose();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
