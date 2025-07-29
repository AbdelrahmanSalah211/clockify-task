using task.Models;

namespace task.Services
{
  public interface IUserService
  {
    Task<User> CreateUserAsync(User user);
  }
}