namespace ClockifyTask.Application.DTOs
{
    public record TimeEntryDto
    {
        public int Id { get; init; }
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
        public string? ClockifyTimeEntryId { get; init; }
        public int? UserId { get; init; }
        public int AssignedTaskId { get; init; }
        public int? ProjectId { get; init; }
    }

}
