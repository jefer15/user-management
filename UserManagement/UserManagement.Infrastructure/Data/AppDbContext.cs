using Microsoft.EntityFrameworkCore;
using UserManagement.Infrastructure.Entities;

namespace UserManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Name).HasMaxLength(100).IsRequired();
                entity.Property(u => u.Gender).HasMaxLength(1).IsRequired();
            });
        }
    }
}
