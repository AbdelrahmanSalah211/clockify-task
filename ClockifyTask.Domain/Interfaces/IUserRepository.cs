using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserSync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
