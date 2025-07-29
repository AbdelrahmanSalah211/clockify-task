using task.Models;

namespace task.Services
{
  public interface IProjectService
  {
    Task<Project> CreateProjectAsync(Project project);
  }
}