using task.Models;
using task.Repositories;

namespace task.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly HttpClient _http;
        private readonly ClockifySettings _settings;

        public ProjectService(IProjectRepository projectRepository, HttpClient http, ClockifySettings settings)
        {
            _projectRepository = projectRepository;
            _http = http;
            _settings = settings;
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            await _projectRepository.CreateAsync(project);
            var body = new
            {
                name = project.Name
            };
            var url = $"https://api.clockify.me/api/v1/workspaces/{_settings.WorkspaceId}/projects";
            var response = await _http.PostAsJsonAsync(url, body);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            project.ClockifyId = json?["id"]?.ToString();
            await _projectRepository.SaveChangesAsync();
            return project;
        }
    }
}
