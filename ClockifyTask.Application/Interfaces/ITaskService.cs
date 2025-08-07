using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.DTOs.Task_Dto;

namespace ClockifyTask.Application.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> CreateAsync(CreateTaskDto taskDto);
    }
}
