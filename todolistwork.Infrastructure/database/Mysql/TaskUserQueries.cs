using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolistwork.Infrastructure.database.Mysql
{
    public static class TaskUserQueries
    {
        public static string AddTaskUser =>
            @"INSERT INTO [TaskUser] ([Name], [Description], [Status], [Priority],[NeedToDo],[StartTime],[EndTime],[UserId]) 
				VALUES (@Name, @Description, @Status, @Priority, @NeedToDo,@StartTime,@EndTime,@UserId)";
        public static string TaskUserByNeedToDo => "SELECT * FROM [TaskUser] (NOLOCK) WHERE [NeedToDo] = true AND [UserId]=@UserId";
        public static string TaskUserById => "SELECT * FROM [TaskUser] (NOLOCK) WHERE [Id] = @Id AND [UserId]=@UserId";
        public static string UpdateTaskUser =>
           @"UPDATE [TaskUser] 
            SET [Name]=@Name,
                [Description]=@Description, 
                [Status]=@Status, 
                [Priority]=@Priority,
                [NeedToDo]=@NeedToDo,
                [StartTime]=@StartTime,
                [EndTime]=@EndTime
            WHERE [Id] = @Id AND [UserId]=@UserId ";
        public static string DeleteTaskUser => "DELETE FROM [TaskUser] WHERE [Id] = @Id AND [UserId]=@UserId";

        public static string AllTaskUser => "SELECT * FROM [TaskUser] (NOLOCK) WHERE [UserId]=@UserId ";
       

    }
}
