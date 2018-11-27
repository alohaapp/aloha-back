namespace Aloha.Services
{
    public interface IUserService
    {
        string GenerateJwtToken(string username, string password);

        string HashPassword(string password);
    }
}