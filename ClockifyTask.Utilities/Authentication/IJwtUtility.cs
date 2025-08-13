namespace ClockifyTask.Utilities.Authentication
{
    public interface IJwtUtility
    {
        public string GenerateToken(int userId, string email);
    }
}
