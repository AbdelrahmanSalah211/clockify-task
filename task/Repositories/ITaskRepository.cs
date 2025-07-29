using task.Models;

public interface ITaskRepository
{
  Task<task.Models.Task> CreateTaskAsync(task.Models.Task task);
  Task<task.Models.Task> SaveChangesAsync();
  Task<task.Models.Task> GetByIdAsync(int id);

}