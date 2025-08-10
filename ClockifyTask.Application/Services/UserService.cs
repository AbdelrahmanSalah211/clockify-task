using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Domain.Interfaces;

namespace ClockifyTask.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> CreateAsync(CreateUserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                ClockifyUserId = userDto.ClockifyUserId
            };

            var result = await _userRepository.CreateUserSync(user);
            await _unitOfWork.SaveChangesAsync();
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
