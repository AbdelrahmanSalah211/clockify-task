namespace ClockifyTask.Application.DTOs
{
    public record CreateAssignedTaskDto
    {
        public required string Name { get; init; }
        public required int EstimatedHours { get; init; }
        public int UserId { get; init; }
        public int ProjectId { get; init; }
    }
}