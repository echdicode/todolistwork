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
            @"INSERT INTO task_user (Id,Name, Description, Status, Priority,NeedToDo,StartTime,EndTime,UserId) 
				VALUES (@Id,@Name, @Description, @Status, @Priority, @NeedToDo,@StartTime,@EndTime,@UserId);
               ";
        public static string LastInsertTaskUser =>
            @" SELECT * FROM task_user WHERE Id = LAST_INSERT_ID() AND UserId=@UserId;";
        public static string TaskUserByNeedToDo => "SELECT * FROM task_user  WHERE NeedToDo = true AND UserId=@UserId";
        public static string TaskUserById => "SELECT * FROM task_user  WHERE Id = @Id ;";
        public static string UpdateTaskUser =>
           @"UPDATE task_user 
            SET Name=@Name,
                Description=@Description, 
                Status=@Status, 
                Priority=@Priority,
                NeedToDo=@NeedToDo,
                StartTime=@StartTime,
                EndTime=@EndTime
            WHERE Id = @Id AND UserId=@UserId ";
        public static string DeleteTaskUser => "DELETE FROM task_user WHERE Id = @Id";

        public static string AllTaskUser => "SELECT * FROM task_user WHERE UserId=@UserId ;";
       // public static string AllTaskUser => "SELECT * FROM task_user ";


    }
}
