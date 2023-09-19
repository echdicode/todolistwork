
namespace todolistwork.Core.Models
{
    public class ChangePasswordBody
    {
        
        public string OldPassword { get; set; }
        public string NewPassword { get; set;}
        public string RetypedPassword { get; set; }

    }
}
