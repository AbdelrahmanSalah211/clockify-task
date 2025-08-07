namespace ClockifyTask.Application.DTOs
{
    public record UserDto
    {
        public int Id { get; init; }
        public required string Name { get; init; }
        public string? ClockifyUserId { get; init; }
    }

    public record CreateUserDto
    {
        public required string Name { get; init; }
        public string? ClockifyUserId { get; init; }
    }
}
