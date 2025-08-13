namespace ClockifyTask.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ClockifyUserId { get; set; }
        public List<AssignedTask> AssignedTasks { get; set; } = new List<AssignedTask>();
        public List<TimeEntry>? TimeEntries { get; set; } = new List<TimeEntry>();
    }
}
