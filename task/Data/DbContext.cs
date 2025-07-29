using Microsoft.EntityFrameworkCore;
using task.Models;

namespace task.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<TimeEntry> TimeEntries { get; set; }
  }
}