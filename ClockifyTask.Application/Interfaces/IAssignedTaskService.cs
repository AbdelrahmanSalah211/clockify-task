using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface IAssignedTaskService
    {
        Task<AssignedTaskDto> CreateAsync(CreateAssignedTaskDto taskDto);
    }
}
