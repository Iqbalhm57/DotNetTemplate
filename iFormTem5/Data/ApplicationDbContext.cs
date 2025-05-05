using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using iFormTem5.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace iFormTem5.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private ModelBuilder modelBuilder;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TemplateTag> TemplateTags { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
         

            base.OnModelCreating(builder);

            // Configure IdentityRole if needed
            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(m => m.ConcurrencyStamp).HasColumnType("longtext");
            });

            // Configure ApplicationUser instead of IdentityUser
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(m => m.ConcurrencyStamp).HasColumnType("longtext");
                entity.Property(m => m.SecurityStamp).HasColumnType("longtext");
                entity.Property(m => m.PasswordHash).HasColumnType("longtext");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.Value).HasColumnType("longtext");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.ProviderDisplayName).HasColumnType("longtext");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.Property(m => m.ClaimType).HasColumnType("longtext");
                entity.Property(m => m.ClaimValue).HasColumnType("longtext");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.Property(m => m.ClaimType).HasColumnType("longtext");
                entity.Property(m => m.ClaimValue).HasColumnType("longtext");
            });
        }
    }
}
