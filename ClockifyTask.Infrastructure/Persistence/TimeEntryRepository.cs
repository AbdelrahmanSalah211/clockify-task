using Microsoft.EntityFrameworkCore;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Infrastructure.Persistence
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<TimeEntry> CreateTimeEntrySync(TimeEntry timeEntry)
        {
            _context.TimeEntries.Add(timeEntry);
            return System.Threading.Tasks.Task.FromResult(timeEntry);
        }

        public async Task<IEnumerable<TimeEntry>> GetAllForReportAsync()
        {
            return await _context.TimeEntries
                .Include(t => t.User)
                .Include(t => t.Task)
                .ThenInclude(task => task!.Project)
                .ToListAsync();
        }
    }
}
