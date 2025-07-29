using Microsoft.EntityFrameworkCore;

namespace task.Models
{
  public class Task
  {
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int EstimatedHours { get; set; }
    public int UserId { get; set; }
    public int ProjectId { get; set; }
    public required Project Project { get; set; }
    public required User User { get; set; }
  }
}