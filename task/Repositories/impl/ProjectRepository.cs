using task.Models;
using task.Data;
using Microsoft.EntityFrameworkCore;

namespace task.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return _context.Projects.Local.FirstOrDefault();
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.TimeEntries)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}