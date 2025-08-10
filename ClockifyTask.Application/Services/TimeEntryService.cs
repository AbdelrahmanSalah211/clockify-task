using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITrackingApiProvider _trackingApiProvider;

        public TimeEntryService(IUnitOfWork unitOfWork, ITrackingApiProvider trackingApiService)
        {
            _unitOfWork = unitOfWork;
            _trackingApiProvider = trackingApiService;
        }

        public async Task<TimeEntryDto> CreateAsync(CreateTimeEntryDto timeEntryDto)
        {
            var timeEntry = new TimeEntry
            {
                Start = timeEntryDto.Start,
                End = timeEntryDto.End,
                UserId = timeEntryDto.UserId,
                AssignedTaskId = timeEntryDto.AssignedTaskId,
                ProjectId = timeEntryDto.ProjectId
            };

            await _unitOfWork.TimeEntries.CreateTimeEntrySync(timeEntry);

            var assignedTask = await _unitOfWork.AssignedTasks.GetByIdAsync(timeEntry.AssignedTaskId);
            if (assignedTask?.ClockifyTaskId == null)
            {
                throw new Exception("Cannot sync time entry to Clockify. Missing Clockify Task ID.");
            }

            var project = await _unitOfWork.Projects.GetByIdAsync(assignedTask.ProjectId);

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
