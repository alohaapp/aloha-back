namespace Aloha.Services
{
    public interface ISecurityService
    {
        string GenerateJwtToken(string username, string password);

        string HashPassword(string password);
    }
}