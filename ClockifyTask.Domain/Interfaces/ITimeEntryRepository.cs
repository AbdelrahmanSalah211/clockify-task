using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Domain.Interfaces
{
    public interface ITimeEntryRepository
    {
        Task<TimeEntry> CreateTimeEntryAsync(TimeEntry timeEntry);
        Task<TimeEntry?> SaveChangesAsync();
        Task<IEnumerable<TimeEntry>> GetAllForReportAsync();
    }
}
