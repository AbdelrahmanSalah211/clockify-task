namespace ClockifyTask.Application.DTOs
{
    public record TaskDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public required int EstimatedHours { get; init; }
        public int UserId { get; init; }
        public int ProjectId { get; init; }
    }

}
