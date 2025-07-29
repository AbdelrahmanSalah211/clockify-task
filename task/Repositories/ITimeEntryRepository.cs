using task.Models;

public interface ITimeEntryRepository
{
  Task<TimeEntry> CreateTimeEntryAsync(TimeEntry timeEntry);
}