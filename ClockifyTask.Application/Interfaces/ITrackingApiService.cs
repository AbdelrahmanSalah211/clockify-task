public interface ITrackingApiService
{
    Task<string> CreateTrackingProjectAsync(ProjectTrackingDto projectDto);
    Task<string> CreateTrackingTaskAsync(TaskTrackingDto taskDto);
    Task<string> CreateTrackingTimeEntryAsync(TimeEntryTrackingDto timeEntryDto);
}