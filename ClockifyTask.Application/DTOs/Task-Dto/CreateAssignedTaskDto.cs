namespace ClockifyTask.Application.DTOs
{
    public record CreateAssignedTaskDto
    {
        public required string Name { get; init; }
        public required int EstimatedHours { get; init; }
        public required int UserId { get; init; }
    }
}