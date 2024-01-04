using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace wBialy.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EventPost> EventPosts { get; set; }
        public DbSet<EventTag> EventTags { get; set; }
        public DbSet<LFPost> LFPosts { get; set; }
        public DbSet<LFTag> LFTags { get; set; }
        public DbSet<GastroPost> GastroPosts { get; set; }
        public DbSet<GastroTag> GastroTags { get; set; }
        public DbSet<OnSitePost> OnSitePosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .UseTptMappingStrategy();
            modelBuilder.Entity<Tag>()
                .UseTptMappingStrategy();
            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostId);
            modelBuilder.Entity<Role>()
                .HasKey(p => p.RoleId);
            modelBuilder.Entity<User>()
                .HasKey(p => p.UserId);
            modelBuilder.Entity<Post>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Post>()
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<Tag>()
                .HasKey(p => p.TagId);
            modelBuilder.Entity<LFTag>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<EventTag>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<GastroTag>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(p => p.OwnedPosts)
                .HasPrincipalKey(p => p.UserId)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                .HasMany(p => p.LikedPosts)
                .WithMany(p => p.LikedBy)
                .UsingEntity<UserLikedPost>(l => l.HasOne<Post>().WithMany().HasForeignKey(y => y.PostId).OnDelete(DeleteBehavior.NoAction),
                r => r.HasOne<User>().WithMany().HasForeignKey(y => y.UserId).OnDelete(DeleteBehavior.Cascade));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
