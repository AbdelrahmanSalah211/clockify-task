using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateAsync(string clockifyId, CreateUserDto userDto);
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
