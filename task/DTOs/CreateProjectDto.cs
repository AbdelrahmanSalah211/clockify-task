namespace task.DTOs
{
    public class CreateProjectDto
    {
        public required string Name { get; set; }
        public string? ClockifyId { get; set; }
    }
}
