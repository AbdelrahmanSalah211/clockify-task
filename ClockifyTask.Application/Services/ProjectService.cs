using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITrackingApiProvider _trackingApiProvider;

        public ProjectService(IProjectRepository projectRepository, ITrackingApiProvider trackingApiProvider)
        {
            _projectRepository = projectRepository;
            _trackingApiProvider = trackingApiProvider;
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto)
        {

            var project = new Project
            {
                Name = projectDto.Name,
            };

            await _projectRepository.CreateProjectAsync(project);

            var projectTracking = new ProjectTrackingDto
            {
                name = projectDto.Name,
            };
            project.ClockifyId = await _trackingApiProvider.CreateTrackingProjectAsync(projectTracking);
            var result = await _projectRepository.SaveChangesAsync();

            return MapToDto(result);
        }

        private static ProjectDto MapToDto(Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                ClockifyId = project.ClockifyId
            };
        }
    }
}
