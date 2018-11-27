using Aloha.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aloha.Models.Contexts
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
                    Id = 1,
                    UserName = "admin",
                    PasswordHash = "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918" // admin
                });
        }
    }
}