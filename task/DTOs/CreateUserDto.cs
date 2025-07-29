namespace task.DTOs
{
    public class CreateUserDto
    {
        public required string Name { get; set; }
        public required string ClockifyUserId { get; set; }
    }
}