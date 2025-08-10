using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Domain.Interfaces
{
    public interface ITimeEntryRepository
    {
        Task<TimeEntry> CreateTimeEntrySync(TimeEntry timeEntry);
        Task<IEnumerable<TimeEntry>> GetAllForReportAsync();
    }
}
