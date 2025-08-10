using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class AssignedTaskService : IAssignedTaskService
    {
        private readonly IAssignedTaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepo;
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrackingApiProvider _trackingApiProvider;

        public AssignedTaskService(IUnitOfWork unitOfWork, IAssignedTaskRepository taskRepository, IProjectRepository projectRepo, IUserRepository userRepo, ITrackingApiProvider trackingApiService)
        {
            _unitOfWork = unitOfWork;
            _taskRepository = taskRepository;
            _projectRepo = projectRepo;
            _userRepo = userRepo;
            _trackingApiProvider = trackingApiService;
        }

        public async Task<AssignedTaskDto> CreateAsync(CreateAssignedTaskDto taskDto)
        {
            var task = new AssignedTask
            {
                Name = taskDto.Name,
                EstimatedHours = taskDto.EstimatedHours,
                UserId = taskDto.UserId,
                ProjectId = taskDto.ProjectId
            };

            await _taskRepository.CreateTaskSync(task);
            
            var project = await _projectRepo.GetByIdAsync(task.ProjectId);
            if (project?.ClockifyId == null)
            {
                throw new Exception("Cannot sync task to Clockify. Missing Clockify Project ID.");
            }

            var user = await _userRepo.GetByIdAsync(task.UserId);
            if (user?.ClockifyUserId == null)
            {
                throw new Exception("Cannot sync task to Clockify. Missing Clockify User ID.");
            }

            var taskTracking = new TaskTrackingDto
            {
                name = task.Name,
                estimatedHours = task.EstimatedHours,
                projectId = project.ClockifyId,
                assigneeIds = new List<string> { user.ClockifyUserId }
            };
            task.ClockifyTaskId = await _trackingApiProvider.CreateTrackingTaskAsync(taskTracking);
            await _unitOfWork.SaveChangesAsync();
            return MapToDto(task);
        }

        private static AssignedTaskDto MapToDto(AssignedTask task)
        {
            return new AssignedTaskDto
            {
                Id = task.Id,
                Name = task.Name,
                EstimatedHours = task.EstimatedHours,
                UserId = task.UserId,
                ProjectId = task.ProjectId
            };
        }
    }
}
