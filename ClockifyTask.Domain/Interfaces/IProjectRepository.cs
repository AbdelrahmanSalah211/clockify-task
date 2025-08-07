using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> CreateProjectAsync(Project project);
        Task<Project> SaveChangesAsync();
        Task<Project?> GetByIdAsync(int id);
    }
}
