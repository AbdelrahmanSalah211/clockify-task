    public record CreateTimeEntryDto
    {
        public DateTime Start { get; init; }
        public DateTime End { get; init; }
        public int UserId { get; init; }
        public int TaskId { get; init; }
        public int ProjectId { get; init; }
    }