using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Aloha.Models.Contexts;
using Microsoft.IdentityModel.Tokens;

namespace Aloha.Services
{
    public interface IUserService
    {
        string Authenticate(string username, string password);

        string HashPassword(string password);
    }

    public class UserService : IUserService
    {
        private readonly AlohaContext dbContext;

        public UserService(AlohaContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string Authenticate(string username, string password)
        {
            string passwordHash = SHA256HexHashString(password);
            var user = dbContext.Users.SingleOrDefault(x => x.UserName == username && x.PasswordHash == passwordHash);

            if (user == null)
            {
                return null;
            }

            var key = Encoding.ASCII.GetBytes("aloha-test_123456789");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string HashPassword(string password)
        {
            return SHA256HexHashString(password);
        }

        private string SHA256HexHashString(string str)
        {
            string hashString;

            using (var sha256 = SHA256Managed.Create())
            {
                var hash = sha256.ComputeHash(Encoding.Default.GetBytes(str));
                hashString = ToHex(hash, false);
            }

            return hashString;
        }

        private string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }
    }
}