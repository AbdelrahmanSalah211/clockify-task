namespace ClockifyTask.Application.DTOs
{
    public record CreateProjectDto
    {
        public required string Name { get; init; }
    }
}