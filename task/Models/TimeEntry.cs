using Microsoft.EntityFrameworkCore;

namespace task.Models
{
  public class TimeEntry
  {
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int UserId { get; set; }
    public int TaskId { get; set; }
    public int ProjectId { get; set; }
    public required User User { get; set; }
    public required Task Task { get; set; }
    public required Project Project { get; set; }
  }
}