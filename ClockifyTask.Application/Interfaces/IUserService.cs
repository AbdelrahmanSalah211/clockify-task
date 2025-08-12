using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(CreateUserDto userDto);
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
