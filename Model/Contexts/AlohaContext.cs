using Microsoft.EntityFrameworkCore;

namespace Aloha.Models.Contexts
{
    public class AlohaContext : DbContext
    {
        public AlohaContext(DbContextOptions<AlohaContext> options) : base(options)
        {
        }
    }
}