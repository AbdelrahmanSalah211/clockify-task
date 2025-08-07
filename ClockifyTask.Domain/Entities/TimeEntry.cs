namespace ClockifyTask.Domain.Entities
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? ClockifyTimeEntryId { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public User? User { get; set; }
        public Task? Task { get; set; }
        public Project? Project { get; set; }
    }
}
