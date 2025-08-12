namespace ClockifyTask.Application.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(int userId, string email);
    }
}