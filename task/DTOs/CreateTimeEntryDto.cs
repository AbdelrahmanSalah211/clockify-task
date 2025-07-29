namespace task.DTOs
{
    public class CreateTimeEntryDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
    }
}
