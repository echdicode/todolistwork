

namespace todolistwork.Core.Entities
{
    public class TaskUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? Priority { get; set; }
        public bool? NeedToDo { get; set; }
        public string UserId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
