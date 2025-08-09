
using System.Net.Http.Json;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.Infrastructure.Providers;
public class ClockifyApiProvider : ITrackingApiProvider
{
    private readonly HttpClient _http;
    private readonly ClockifySettings _settings;
    private static readonly string BaseUrl = "https://api.clockify.me/api/v1/workspaces";

    public ClockifyApiProvider(HttpClient http, ClockifySettings settings)
    {
        _http = http;
        _settings = settings;
        _http.DefaultRequestHeaders.Clear();
        _http.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
    }
    public async Task<string> CreateTrackingProjectAsync(ProjectTrackingDto projectDto)
    {
        var url = $"{BaseUrl}/{_settings.WorkspaceId}/projects";
        var response = await _http.PostAsJsonAsync(url, projectDto);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return json?["id"]?.ToString();
    }

    public async Task<string> CreateTrackingTaskAsync(TaskTrackingDto taskDto)
    {
        var url = $"{BaseUrl}/{_settings.WorkspaceId}/projects/{taskDto.projectId}/tasks";
        var response = await _http.PostAsJsonAsync(url, new { name = taskDto.name, estimate = $"PT{taskDto.estimatedHours}H", assigneeIds = taskDto.assigneeIds });
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return json?["id"]?.ToString();
    }

    public async Task<string> CreateTrackingTimeEntryAsync(TimeEntryTrackingDto timeEntryDto)
    {
        var url = $"{BaseUrl}/{_settings.WorkspaceId}/time-entries";
        var response = await _http.PostAsJsonAsync(url, timeEntryDto);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        return json?["id"]?.ToString();
    }
}