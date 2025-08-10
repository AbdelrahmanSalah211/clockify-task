using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Infrastructure.Persistence
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAssignedTaskRepository _assignedTaskRepository;
        private readonly ApplicationDbContext _context;
        private bool _disposed;
        public UnitOfWork(
            IProjectRepository projectRepository,
            ITimeEntryRepository timeEntryRepository,
            IUserRepository userRepository,
            IAssignedTaskRepository assignedTaskRepository,
            ApplicationDbContext context)
        {
            _projectRepository = projectRepository;
            _timeEntryRepository = timeEntryRepository;
            _userRepository = userRepository;
            _assignedTaskRepository = assignedTaskRepository;
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public IProjectRepository Projects => _projectRepository;
        public IUserRepository Users => _userRepository;
        public IAssignedTaskRepository AssignedTasks => _assignedTaskRepository;
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