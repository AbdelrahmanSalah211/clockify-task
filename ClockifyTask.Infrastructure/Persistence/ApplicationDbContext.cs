using Microsoft.EntityFrameworkCore;
using ClockifyTask.Domain.Entities;


namespace ClockifyTask.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<AssignedTask> AssignedTasks { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Project relationships
            modelBuilder.Entity<Project>()
                .HasMany(p => p.AssignedTasks)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.TimeEntries)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            // User relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedTasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.TimeEntries)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            // Task relationships
            modelBuilder.Entity<AssignedTask>()
                .HasMany<TimeEntry>()
                .WithOne(t => t.AssignedTask)
                .HasForeignKey(t => t.AssignedTaskId);
        }
    }
}
