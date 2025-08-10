using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(CreateTaskDto taskDto);
    }
}
