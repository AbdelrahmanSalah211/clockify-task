using task.Models;
using task.Repositories;
using Task = task.Models.Task;

namespace task.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepo;
        private readonly IUserRepository _userRepo;
        private readonly HttpClient _http;
        private readonly ClockifySettings _settings;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepo, IUserRepository userRepo, HttpClient http, ClockifySettings settings)
        {
        _taskRepository = taskRepository;
        _projectRepo = projectRepo;
        _userRepo = userRepo;
        _http = http;
        _settings = settings;
        _http.DefaultRequestHeaders.Clear();
        _http.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
        }
        public async Task<Task> CreateTaskAsync(Task task)
        {
            await _taskRepository.CreateTaskAsync(task);
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

            var body = new
            {
                task.Name,
                estimate = $"PT{task.EstimatedHours}H",
                assigneeIds = user.ClockifyUserId != null ? new[] { user.ClockifyUserId } : null,
            };
            var url = $"https://api.clockify.me/api/v1/workspaces/{_settings.WorkspaceId}/projects/{project.ClockifyId}/tasks";
            var response = await _http.PostAsJsonAsync(url, body);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Clockify API error {response.StatusCode}: {error}");
            }
            var json = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            task.ClockifyTaskId = json?["id"]?.ToString();
            await _taskRepository.SaveChangesAsync();
            return task;
        }

    }
}