
namespace ClockifyTask.Application.Interfaces
{
    public interface IPasswordHandling
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}