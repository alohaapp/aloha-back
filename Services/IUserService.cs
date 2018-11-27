namespace Aloha.Services
{
    public interface IUserService
    {
        string Authenticate(string username, string password);

        string HashPassword(string password);
    }
}