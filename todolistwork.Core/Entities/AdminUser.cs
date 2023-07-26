

namespace todolistwork.Core.Entities
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
