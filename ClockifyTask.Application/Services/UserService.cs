using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                ClockifyUserId = userDto.ClockifyUserId
            };

            var result = await _userRepository.CreateAsync(user);
            return MapToDto(result);
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                ClockifyUserId = user.ClockifyUserId
            };
        }
    }
}
