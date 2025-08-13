using ClockifyTask.Application.DTOs;

namespace ClockifyTask.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task<object> LoginAsync(LoginDto loginDto);
    }
}