using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface ITimeEntryService
    {
        Task<TimeEntryDto> CreateAsync(CreateTimeEntryDto timeEntryDto);
    }
}
