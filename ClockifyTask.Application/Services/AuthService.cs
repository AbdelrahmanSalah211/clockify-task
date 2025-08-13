using ClockifyTask.Application.DTOs;
using ClockifyTask.Application.Interfaces;
using ClockifyTask.Domain.Interfaces;
using ClockifyTask.Application.Security;
using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHandling _passwordHandling;
        private readonly IJwtService _jwtService;
        public AuthService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IJwtService jwtService,
            IPasswordHandling passwordHandling
        )
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _passwordHandling = passwordHandling;
        }
        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            if (string.IsNullOrEmpty(registerDto.Email) || string.IsNullOrEmpty(registerDto.Password))
            {
                return false;
            }

            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return false;
            }

            var hashedPassword = _passwordHandling.HashPassword(registerDto.Password);
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = hashedPassword
            };

            await _userRepository.CreateUserSync(user);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<object> LoginAsync(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new ArgumentException("Email and password must be provided.");
            }

            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new ArgumentException("Invalid email or password.");
            }

            var isPasswordValid = _passwordHandling.VerifyPassword(loginDto.Password, user.Password);
            if (!isPasswordValid)
            {
                throw new ArgumentException("Invalid email or password.");
            }

            var token = _jwtService.GenerateToken(user.Id, user.Email);

            return new { userId = user.Id, email = user.Email, token };
        }
    }
}