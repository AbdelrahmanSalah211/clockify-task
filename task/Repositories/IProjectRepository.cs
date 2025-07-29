using task.Models;



public interface IProjectRepository
{
    Task<Project> CreateAsync(Project project);
    Task<Project> SaveChangesAsync();
    Task<Project?> GetByIdAsync(int id);
}
