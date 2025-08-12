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

        public async Task<UserDto> UpdateAsync(int userId, UpdateUserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userId) ?? throw new KeyNotFoundException("User not found.");

            user.Name = userDto.Name ?? user.Name;
            user.Email = userDto.Email ?? user.Email;
            user.ClockifyUserId = userDto.ClockifyUserId ?? user.ClockifyUserId;

            await _unitOfWork.SaveChangesAsync();
            return MapToDto(user);
        }


        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users;
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ClockifyUserId = user.ClockifyUserId
            };
        }
    }
}
