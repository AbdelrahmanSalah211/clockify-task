using task.Models;
using task.Data;

namespace task.Repositories
{
    public class TimeEntryRepository : ITimeEntryRepository
    {
        private readonly AppDbContext _context;

        public TimeEntryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TimeEntry> CreateTimeEntryAsync(TimeEntry timeEntry)
        {
            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();
            return timeEntry;
        }
    }
}