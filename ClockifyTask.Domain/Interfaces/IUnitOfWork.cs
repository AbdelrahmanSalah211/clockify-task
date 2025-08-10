namespace ClockifyTask.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        IUserRepository Users { get; }
        ITaskRepository Tasks { get; }
        ITimeEntryRepository TimeEntries { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}