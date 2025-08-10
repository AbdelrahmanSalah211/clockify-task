using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Domain.Interfaces
{
    public interface ITimeEntryRepository
    {
        Task<TimeEntry> CreateTimeEntryAsync(TimeEntry timeEntry);
        Task<IEnumerable<TimeEntry>> GetAllForReportAsync();
    }
}
