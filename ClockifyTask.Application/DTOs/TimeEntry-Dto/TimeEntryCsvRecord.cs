public class TimeEntryCsvRecord
{
    public string UserName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string TaskName { get; set; } = string.Empty;
    public int OriginalEstimate { get; set; }
    public double TimeSpent { get; set; }
}