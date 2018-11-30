using Aloha.Model.Entities;
using Aloha.Services;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Model.Contexts
{
    public class AlohaContext : DbContext
    {
        public AlohaContext(DbContextOptions<AlohaContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Workstation> Workstations { get; set; }

        public DbSet<Floor> Floors { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasAlternateKey(u => u.UserName);
            builder.Entity<Worker>()
                .HasIndex(u => u.UserId)
                .IsUnique();

            // Seed
            builder.Entity<User>()
                .HasData(new User
                {
                    Id = -1,
                    UserName = "admin",
                    PasswordHash = SecurityService.SHA256HexHashString("admin")
                });
        }
    }
}