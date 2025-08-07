using ClockifyTask.Domain.Interfaces;
using Task = ClockifyTask.Domain.Entities.Task;

namespace ClockifyTask.Infrastructure.Persistence
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Task> CreateTaskAsync(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return _context.Tasks.Local.FirstOrDefault() ?? new Task { Name = "", EstimatedHours = 0 };
        }

        public async Task<Task> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id) ?? new Task { Name = "", EstimatedHours = 0 };
        }
    }
}
