

using System.Reflection.Metadata.Ecma335;
using todolistwork.Core.Models;
using todolistwork.Core.Unit;

namespace todolistwork.Core.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatedTime { get; set; }
        public string UpdateTime { get; set; }
        public bool IsSuperuser { get; set; }
        public static User Create(RegistrationBody userbody)
        { 
            User user = new()
            {
                Id =  Guid.NewGuid().ToString(),
                UserName = userbody.UserName,
                Email = userbody.Email,
                Password = UnitCore.HashMd5(userbody.Password),
                CreatedTime = UnitCore.GetTimestamp( DateTime.Now),
                UpdateTime = UnitCore.GetTimestamp(DateTime.Now),
                IsSuperuser= false
            };
            return user;
         }
        public static User Create( string Email,string Password, string  UserName )
        {
            User user = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = UserName,
                Email = Email,
                Password = UnitCore.HashMd5(Password),
                CreatedTime = UnitCore.GetTimestamp(DateTime.Now),
                UpdateTime = UnitCore.GetTimestamp(DateTime.Now),
                IsSuperuser = false
            };
            return user;
        }
    }
}
