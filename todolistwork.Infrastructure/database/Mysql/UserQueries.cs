using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolistwork.Infrastructure.database.Mysql
{
    public static class UserQueries
    {
        
        public static string AddUser => @"
        INSERT INTO User (Id, UserName, Email)
        VALUES (@Id, @UserName, @Email);

        INSERT INTO UserAccounts (UserId, AccountType, AccountType)
        VALUES (@UserId, @AccountType, @AccountType);
    ";
        public static string UserById => "SELECT * FROM [TaskUser] (NOLOCK) WHERE [Id] = @Id";
        public static string UpdateTaskUser =>
           @"UPDATE [TaskUser] 
            SET [Name]=@Name,
                [Description]=@Description, 
                [Status]=@Status, 
                [Priority]=@Priority,
                [NeedToDo]=@NeedToDo,
                [StartTime]=@StartTime,
                [EndTime]=@EndTime
            WHERE [Id] = @Id";
        public static string DeleteUser => "DELETE FROM [TaskUser] WHERE [Id] = @Id";

        public static string AllUser => "SELECT * FROM [TaskUser] (NOLOCK)";

    }
}
