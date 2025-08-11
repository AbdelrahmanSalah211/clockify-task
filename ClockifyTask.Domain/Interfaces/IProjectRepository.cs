using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project>  CreateProjectSync(Project project);
        Task<Project?> GetByIdAsync(int id);
    }
}
