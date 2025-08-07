using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepo;
        private readonly ITrackingApiProvider _trackingApiProvider;

        public TimeEntryService(ITimeEntryRepository timeEntryRepository, ITaskRepository taskRepository, IProjectRepository projectRepo, ITrackingApiProvider trackingApiService)
        {
            _timeEntryRepository = timeEntryRepository;
            _taskRepository = taskRepository;
            _projectRepo = projectRepo;
            _trackingApiProvider = trackingApiService;
        }

        public async Task<TimeEntryDto> CreateAsync(CreateTimeEntryDto timeEntryDto)
        {
            var timeEntry = new TimeEntry
            {
                Start = timeEntryDto.Start,
                End = timeEntryDto.End,
                UserId = timeEntryDto.UserId,
                TaskId = timeEntryDto.TaskId,
                ProjectId = timeEntryDto.ProjectId
            };

            await _timeEntryRepository.CreateTimeEntryAsync(timeEntry);
            
            var task = await _taskRepository.GetByIdAsync(timeEntry.TaskId);
            if (task?.ClockifyTaskId == null)
            {
                throw new Exception("Cannot sync time entry to Clockify. Missing Clockify Task ID.");
            }
            
            var project = await _projectRepo.GetByIdAsync(task.ProjectId);
            if (project?.ClockifyId == null)
            {
                throw new Exception("Cannot sync time entry to Clockify. Missing Clockify Project ID.");
            }
            
            var timeEntryTracking = new TimeEntryTrackingDto
            {
                start = timeEntry.Start,
                end = timeEntry.End,
                projectId = project.ClockifyId,
                taskId = task.ClockifyTaskId,
            };
            timeEntry.ClockifyTimeEntryId = await _trackingApiProvider.CreateTrackingTimeEntryAsync(timeEntryTracking);
            var result = await _timeEntryRepository.SaveChangesAsync();
            
            return MapToDto(result);
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
                TaskId = timeEntry.TaskId,
                ProjectId = timeEntry.ProjectId
            };
        }
    }
}
