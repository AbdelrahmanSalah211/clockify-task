using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface IAssignedTaskService
    {
        Task<AssignedTaskDto> CreateAsync(int projectId, CreateAssignedTaskDto taskDto);
        Task<IEnumerable<AssignedTaskDto>> GetAllAsync();
    }
}
