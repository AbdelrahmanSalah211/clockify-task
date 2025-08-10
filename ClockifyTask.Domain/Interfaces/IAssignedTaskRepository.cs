using AssignedTask = ClockifyTask.Domain.Entities.AssignedTask;

namespace ClockifyTask.Domain.Interfaces
{
    public interface IAssignedTaskRepository
    {
        Task<AssignedTask> CreateTaskSync(AssignedTask task);
        Task<AssignedTask?> GetByIdAsync(int id);
    }
}
