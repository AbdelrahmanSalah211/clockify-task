using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<object>> GetAllAsync();
        Task<UserDto> UpdateAsync(int userId, UpdateUserDto userDto);
    }
}
