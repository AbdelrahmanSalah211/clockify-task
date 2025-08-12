namespace ClockifyTask.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ClockifyUserId { get; set; }
        public List<AssignedTask> AssignedTasks { get; set; } = new List<AssignedTask>();
        public List<TimeEntry>? TimeEntries { get; set; } = new List<TimeEntry>();
    }
}
