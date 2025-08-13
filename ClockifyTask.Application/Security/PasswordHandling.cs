using Microsoft.AspNetCore.Identity;
using ClockifyTask.Domain.Entities;
using ClockifyTask.Application.Interfaces;

namespace ClockifyTask.Application.Security
{
    public class PasswordHandling : IPasswordHandling
    {
        private static readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public string HashPassword(string password)
        {
            
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
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
