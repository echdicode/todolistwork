

namespace todolistwork.Core.Models
{
    public class TaskUserBody
    {
        public TaskUserBody()
        {
            Name = string.Empty;
            Description = string.Empty;
            Status = 0;
            Priority = 0;
            NeedToDo = false;
         
            StartTime = string.Empty;
            EndTime = string.Empty;

        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? Priority { get; set; }
        public bool? NeedToDo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
