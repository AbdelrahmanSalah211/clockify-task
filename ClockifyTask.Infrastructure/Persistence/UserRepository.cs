using Microsoft.EntityFrameworkCore;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User> CreateUserSync(User user)
        {
            _context.Users.Add(user);
            return Task.FromResult(user);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
