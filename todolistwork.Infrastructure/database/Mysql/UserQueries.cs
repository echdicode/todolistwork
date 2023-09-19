using Microsoft.VisualBasic;


namespace todolistwork.Infrastructure.database.Mysql
{
    public static class UserQueries
    {
        public static string AddUser => @"INSERT INTO user (Id,UserName, Email,CreatedTime,UpdateTime,Password,IsSuperuser)
                                         VALUES (@Id, @UserName,@Email,@CreatedTime,@UpdateTime,@Password,@IsSuperuser);";
        public static string AllUser => @"SELECT * FROM user WHERE IsSuperuser =false;";
        public static string UserLogin => @"SELECT Id,UserName,Email,IsSuperuser FROM user WHERE Email =@Email AND Password=@Password;";

        public static string UserById => @"SELECT Id,UserName,Email,IsSuperuser FROM user WHERE Id = @Id;";
        public static string UserByEmail => @"SELECT * FROM user WHERE Email = @Email;";
        public static string UpdateUser => @"UPDATE user SET UserName = @UserName WHERE Id = @Id;";
        public static string UpdatePassword => @"UPDATE user SET Password = @Password WHERE Id = @Id;";

        public static string DeleteUser => "DELETE FROM user WHERE Id = @Id";

    }
}
