using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class AssignedTaskService : IAssignedTaskService
    {
        private readonly IAssignedTaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrackingApiProvider _trackingApiProvider;

        public AssignedTaskService(IUnitOfWork unitOfWork, IAssignedTaskRepository taskRepository, IProjectRepository projectRepository, IUserRepository userRepository, ITrackingApiProvider trackingApiProvider)
        {
            _unitOfWork = unitOfWork;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _trackingApiProvider = trackingApiProvider;
        }

        public async Task<AssignedTaskDto> CreateAsync(int projectId, CreateAssignedTaskDto taskDto)
        {
            var task = new AssignedTask
            {
                Name = taskDto.Name,
                EstimatedHours = taskDto.EstimatedHours,
                UserId = taskDto.UserId,
                ProjectId = projectId
            };

            await _taskRepository.CreateTaskSync(task);

            var project = await _projectRepository.GetByIdAsync(task.ProjectId);
            if (project?.ClockifyId == null)
            {
                throw new Exception("Cannot sync task to Clockify. Missing Clockify Project ID.");
            }

            var user = await _userRepository.GetByIdAsync(task.UserId);
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

        public async Task<IEnumerable<AssignedTaskDto>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(MapToDto);
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
