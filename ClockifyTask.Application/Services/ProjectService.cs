using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrackingApiProvider _trackingApiProvider;

        public ProjectService(IUnitOfWork unitOfWork, IProjectRepository projectRepository, ITrackingApiProvider trackingApiProvider)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
            _trackingApiProvider = trackingApiProvider;
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto projectDto)
        {

            var project = new Project
            {
                Name = projectDto.Name,
            };

            await _projectRepository.CreateProjectSync(project);

            var projectTracking = new ProjectTrackingDto
            {
                name = projectDto.Name,
            };
            project.ClockifyId = await _trackingApiProvider.CreateTrackingProjectAsync(projectTracking);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(project);
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
