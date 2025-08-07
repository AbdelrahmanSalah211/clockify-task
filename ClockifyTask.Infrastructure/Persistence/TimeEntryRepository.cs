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

        public async Task<TimeEntry> CreateTimeEntryAsync(TimeEntry timeEntry)
        {
            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();
            return timeEntry;
        }

        public async Task<TimeEntry> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return _context.TimeEntries.Local.FirstOrDefault() ?? new TimeEntry();
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
