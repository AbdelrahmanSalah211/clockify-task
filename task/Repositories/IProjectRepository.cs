using task.Models;



    public interface IProjectRepository
    {
        Task<Project> CreateAsync(Project project);
    }
