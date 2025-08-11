using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface IAssignedTaskService
    {
        Task<AssignedTaskDto> CreateAsync(int projectId, int userId, CreateAssignedTaskDto taskDto);
        Task<IEnumerable<AssignedTaskDto>> GetAllAsync();
    }
}
