public record TaskTrackingDto
{
    public string name { get; set; }
    public decimal estimatedHours { get; set; }
    public List<string> assigneeIds { get; set; }
    public string projectId { get; set; }
}