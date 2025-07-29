using task.Models;
using Task = task.Models.Task;

namespace task.Services
{
  public interface ITaskService
  {
    Task<Task> CreateTaskAsync(Task task);
  }
}