namespace ClockifyTask.Application.DTOs
{
    public record ProjectDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public string? ClockifyId { get; init; }
    }
}
