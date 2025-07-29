using task.Models;

public interface IUserRepository
{
  Task<User> CreateAsync(User user);
  Task<User?> GetByIdAsync(int id);
}