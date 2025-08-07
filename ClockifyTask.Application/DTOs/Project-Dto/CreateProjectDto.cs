namespace ClockifyTask.Application.DTOs;

public record CreateProjectDto
{
    public required string Name { get; init; }
    public string? ClockifyId { get; init; }
}