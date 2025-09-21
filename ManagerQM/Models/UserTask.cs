namespace ManagerQM.Models
{
    public enum TaskStatus
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2
    }

    public class UserTask
    {
        public int Id { get; set; }
        public string TaskInfo { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.Pending;

        public DateTime? Deadline { get; set; }

        public UserTask() { }

        public UserTask(int id, string taskInfo, TaskStatus status, DateTime? deadline)
        {
            Id = id;
            TaskInfo = taskInfo;
            Status = status;
            Deadline = deadline;
        }
    }
}
