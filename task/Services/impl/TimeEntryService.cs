using task.Models;
using task.Repositories;

namespace task.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepo;
        private readonly HttpClient _http;
        private readonly ClockifySettings _settings;

        public TimeEntryService(ITimeEntryRepository timeEntryRepository, ITaskRepository taskRepository, IProjectRepository projectRepo, HttpClient http, ClockifySettings settings)
        {
        _timeEntryRepository = timeEntryRepository;
        _taskRepository = taskRepository;
        _projectRepo = projectRepo;
        _http = http;
        _settings = settings;
        _http.DefaultRequestHeaders.Clear();
        _http.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
        }
        public async Task<TimeEntry> CreateTimeEntryAsync(TimeEntry timeEntry)
        {
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
            var body = new
            {
                projectId = project.ClockifyId,
                taskId = task.ClockifyTaskId,
                start = timeEntry.Start,
                end = timeEntry.End,
            };
            var url = $"https://api.clockify.me/api/v1/workspaces/{_settings.WorkspaceId}/time-entries";
            var response = await _http.PostAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            timeEntry.ClockifyTimeEntryId = json?["id"]?.ToString();
            await _timeEntryRepository.SaveChangesAsync();
            return timeEntry;
        }

    }
}