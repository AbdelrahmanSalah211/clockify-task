using Microsoft.EntityFrameworkCore;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Infrastructure.Persistence
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<Project> CreateProjectSync(Project project)
        {
            _context.Projects.Add(project);
            return Task.FromResult(project);
        }

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }
    }
}
