using Aloha.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Models.Contexts
{
    public class AlohaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Workstation> Workstations { get; set; }
        public DbSet<Floor> Floors { get; set; }

        public AlohaContext(DbContextOptions<AlohaContext> options) : base(options)
        {
            
        }
    }
}