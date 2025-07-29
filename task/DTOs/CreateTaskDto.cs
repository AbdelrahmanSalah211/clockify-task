namespace task.DTOs
{
    public class CreateTaskDto
    {
        public required string Name { get; set; }
        public int EstimatedHours { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
    }
}