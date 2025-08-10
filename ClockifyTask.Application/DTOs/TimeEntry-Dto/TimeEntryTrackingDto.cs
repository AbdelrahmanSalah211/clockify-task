namespace ClockifyTask.Application.DTOs
{
    public record TimeEntryTrackingDto
    {
        public string assignedTaskId { get; set; }
        public string projectId { get; set; }
        public string userId { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}