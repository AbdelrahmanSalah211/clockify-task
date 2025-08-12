using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface ITimeEntryService
    {
        Task<TimeEntryDto> CreateAsync(int assignedTaskId, CreateTimeEntryDto timeEntryDto);
        Task<IEnumerable<TimeEntryDto>> GetAllAsync();
    }
}
