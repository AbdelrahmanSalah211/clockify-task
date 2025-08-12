using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IAssignedTaskRepository _assignedTaskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITrackingApiProvider _trackingApiProvider;

        public TimeEntryService(IUnitOfWork unitOfWork, ITimeEntryRepository timeEntryRepository, IAssignedTaskRepository assignedTaskRepository, IProjectRepository projectRepository, ITrackingApiProvider trackingApiService)
        {
            _unitOfWork = unitOfWork;
            _timeEntryRepository = timeEntryRepository;
            _assignedTaskRepository = assignedTaskRepository;
            _projectRepository = projectRepository;
            _trackingApiProvider = trackingApiService;
        }

        public async Task<TimeEntryDto> CreateAsync(int assignedTaskId, CreateTimeEntryDto timeEntryDto)
        {
            var timeEntry = new TimeEntry
            {
                Start = timeEntryDto.Start,
                End = timeEntryDto.End,
                UserId = timeEntryDto.UserId,
                AssignedTaskId = assignedTaskId,
                ProjectId = timeEntryDto.ProjectId
            };

            await _timeEntryRepository.CreateTimeEntrySync(timeEntry);

            var assignedTask = await _assignedTaskRepository.GetByIdAsync(timeEntry.AssignedTaskId);
            if (assignedTask?.ClockifyTaskId == null)
            {
                throw new Exception("Cannot sync time entry to Clockify. Missing Clockify Task ID.");
            }

            var project = await _projectRepository.GetByIdAsync(assignedTask.ProjectId);

            if (project?.ClockifyId == null)
            {
                throw new Exception("Cannot sync time entry to Clockify. Missing Clockify Project ID.");
            }
            
            var timeEntryTracking = new TimeEntryTrackingDto
            {
                start = timeEntry.Start,
                end = timeEntry.End,
                projectId = project.ClockifyId,
                assignedTaskId = assignedTask.ClockifyTaskId,
            };
            timeEntry.ClockifyTimeEntryId = await _trackingApiProvider.CreateTrackingTimeEntryAsync(timeEntryTracking);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(timeEntry);
        }

        public async Task<IEnumerable<TimeEntryDto>> GetAllAsync()
        {
            var timeEntries = await _timeEntryRepository.GetAllAsync();
            return timeEntries.Select(MapToDto);
        }

        private static TimeEntryDto MapToDto(TimeEntry timeEntry)
        {
            return new TimeEntryDto
            {
                Id = timeEntry.Id,
                Start = timeEntry.Start,
                End = timeEntry.End,
                ClockifyTimeEntryId = timeEntry.ClockifyTimeEntryId,
                UserId = timeEntry.UserId,
                AssignedTaskId = timeEntry.AssignedTaskId,
                ProjectId = timeEntry.ProjectId
            };
        }
    }
}
