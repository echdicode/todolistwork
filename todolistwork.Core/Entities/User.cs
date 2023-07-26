

namespace todolistwork.Core.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserAccounts Account { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
