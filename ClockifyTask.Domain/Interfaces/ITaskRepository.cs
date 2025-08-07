using Task = ClockifyTask.Domain.Entities.Task;

namespace ClockifyTask.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<Task> CreateTaskAsync(Task task);
        Task<Task> SaveChangesAsync();
        Task<Task> GetByIdAsync(int id);
    }
}
