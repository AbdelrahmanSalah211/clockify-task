using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserSync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<object>> GetAllAsync();
    }
}
