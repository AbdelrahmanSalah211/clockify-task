using task.Models;

public interface ITimeEntryRepository
{
  Task<TimeEntry> CreateAsync(TimeEntry timeEntry);
}