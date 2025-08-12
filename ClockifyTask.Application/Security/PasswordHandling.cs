using Microsoft.AspNetCore.Identity;
using ClockifyTask.Domain.Entities;

namespace ClockifyTask.Application.Utilities
{
    public static class PasswordHandling
    {
        private static readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public static string HashPassword(string password)
        {
            
            return _passwordHasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {

                var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
                return result == PasswordVerificationResult.Success || 
                       result == PasswordVerificationResult.SuccessRehashNeeded;
            }
            catch
            {
                return false;
            }
        }
    }
}
