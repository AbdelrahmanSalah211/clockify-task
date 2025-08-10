using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Infrastructure.Persistence
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ApplicationDbContext _context;
        private bool _disposed;
        public UnitOfWork(
            IProjectRepository projectRepository,
            ITimeEntryRepository timeEntryRepository,
            IUserRepository userRepository,
            ITaskRepository taskRepository,
            ApplicationDbContext context)
        {
            _projectRepository = projectRepository;
            _timeEntryRepository = timeEntryRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public IProjectRepository Projects => _projectRepository;
        public IUserRepository Users => _userRepository;
        public ITaskRepository Tasks => _taskRepository;
        public ITimeEntryRepository TimeEntries => _timeEntryRepository;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}