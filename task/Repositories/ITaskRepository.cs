using task.Models;

public interface ITaskRepository
{
  Task<task.Models.Task> CreateTaskAsync(task.Models.Task task);
}