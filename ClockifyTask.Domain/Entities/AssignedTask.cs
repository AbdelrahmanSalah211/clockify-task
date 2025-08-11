namespace ClockifyTask.Domain.Entities
{
    public class AssignedTask
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int EstimatedHours { get; set; }
        public string? ClockifyTaskId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public User? User { get; set; }
    }
}
