using ClockifyTask.Domain.Interfaces;
using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Infrastructure.Persistence
{
    public class AssignedTaskRepository : IAssignedTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public AssignedTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<AssignedTask> CreateTaskSync(AssignedTask task)
        {
            _context.AssignedTasks.Add(task);
            return Task.FromResult(task);
        }

        public async Task<AssignedTask?> GetByIdAsync(int id)
        {
            return await _context.AssignedTasks.FindAsync(id);
        }
    }
}
