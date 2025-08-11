namespace ClockifyTask.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ClockifyId { get; set; }
        public List<AssignedTask> AssignedTasks { get; set; } = new List<AssignedTask>();
        public List<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    }
}
