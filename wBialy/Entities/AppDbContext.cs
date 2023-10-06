using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace wBialy.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Role>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<User>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Post>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Post>()
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(250);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
