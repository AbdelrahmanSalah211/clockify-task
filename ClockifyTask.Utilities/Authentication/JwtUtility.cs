using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using ClockifyTask.Utilities.Authentication;

namespace ClockifyTask.Utilities.Authentication
{
    public class JwtUtility : IJwtUtility
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expiresIn;

        public JwtUtility(IConfiguration configuration)
        {
            _secretKey = configuration["JWT:Key"] ?? throw new ArgumentNullException("JWT:Key");
            _issuer = configuration["JWT:Issuer"] ?? throw new ArgumentNullException("JWT:Issuer");
            _audience = configuration["JWT:Audience"] ?? throw new ArgumentNullException("JWT:Audience");
            _expiresIn = configuration["JWT:ExpiresIn"] ?? throw new ArgumentNullException("JWT:ExpiresIn");
        }

        public string GenerateToken(int userId, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expiresInHours = int.TryParse(_expiresIn, out var hours) ? hours : 1;

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expiresInHours),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
