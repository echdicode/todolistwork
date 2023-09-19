

using todolistwork.Core.Models;
using todolistwork.Core.Unit;

namespace todolistwork.Core.Entities
{
    public class TaskUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
        public int? Priority { get; set; }
        public bool? NeedToDo { get; set; }
        public string UserId { get; set; }
        public string CreatedTime { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string UpdateTime { get; set; }
        public TaskUser () {
         

        }

        public TaskUser (TaskUserBody taskUserBody,string userId)
        {

            Id = UnitCore.Random();
            Name = taskUserBody.Name;
            Description = taskUserBody.Description;
            Status = taskUserBody.Status;
            Priority = taskUserBody.Priority;
            NeedToDo = taskUserBody.NeedToDo;
            /*  CreatedTime = UnitCore.GetTimestamp(taskUserBody.CreatedTime);
              StartTime = UnitCore.GetTimestamp(taskUserBody.StartTime);
              EndTime = UnitCore.GetTimestamp(taskUserBody.EndTime);
              UpdateTime = UnitCore.GetTimestamp(DateTime.Now);*/
            CreatedTime = UnitCore.GetTimestamp(DateTime.Now);
            StartTime = taskUserBody.StartTime;
            EndTime = taskUserBody.EndTime;
            UpdateTime = UnitCore.GetTimestamp(DateTime.Now); 
            UserId = userId;


        }
        public TaskUser(TaskUserBody taskUserBody, string userId,string id)
        {

            Id =id;
            Name = taskUserBody.Name;
            Description = taskUserBody.Description;
            Status = taskUserBody.Status;
            Priority = taskUserBody.Priority;
            NeedToDo = taskUserBody.NeedToDo;
            CreatedTime = UnitCore.GetTimestamp(DateTime.Now).ToString();
            StartTime = taskUserBody.StartTime;
            EndTime = taskUserBody.EndTime;
            UpdateTime = UnitCore.GetTimestamp(DateTime.Now).ToString();
            UserId = userId;


        }


    }
}
