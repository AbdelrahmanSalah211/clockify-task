using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface ITimeEntryService
    {
        Task<TimeEntryDto> CreateAsync(int projectId, int assignedTaskId, int userId, CreateTimeEntryDto timeEntryDto);
    }
}
