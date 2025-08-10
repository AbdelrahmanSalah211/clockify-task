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

        public Task<Task> CreateTaskSync(Task task)
        {
            _context.Tasks.Add(task);
            return System.Threading.Tasks.Task.FromResult(task);
        }

        public async Task<Task?> GetByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }
    }
}
