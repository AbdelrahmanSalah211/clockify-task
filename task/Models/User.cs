using Microsoft.EntityFrameworkCore;

namespace task.Models
{
  public class User
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? ClockifyUserId { get; set; }
    public List<Task> Tasks { get; set; } = new List<Task>();
    public List<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
  }
}