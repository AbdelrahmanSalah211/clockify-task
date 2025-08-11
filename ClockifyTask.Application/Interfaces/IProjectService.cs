using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    }
}
