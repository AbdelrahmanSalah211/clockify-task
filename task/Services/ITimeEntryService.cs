using task.Models;

namespace task.Services
{
  public interface ITimeEntryService
  {
    Task<TimeEntry> CreateTimeEntryAsync(TimeEntry timeEntry);
  }
}