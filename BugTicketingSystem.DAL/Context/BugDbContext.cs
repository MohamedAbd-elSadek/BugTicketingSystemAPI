
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL.Context
{
    public class BugDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public BugDbContext(DbContextOptions<BugDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BugDbContext).Assembly);
            modelBuilder.Entity<User>(e => e.ToTable("Users"));
            modelBuilder.Entity<IdentityRole>(e => e.ToTable("Roles"));
            modelBuilder.Entity<IdentityUserRole<Guid>>(e => e.ToTable("UserRoles"));
            modelBuilder.Entity<IdentityUserClaim<Guid>>(e => e.ToTable("UserClaims"));
            modelBuilder.Entity<IdentityUserLogin<Guid>>(e => e.ToTable("UserLogins"));
            modelBuilder.Entity<IdentityUserToken<Guid>>(e => e.ToTable("UserTokens"));

        }

        // Define your DbSet properties for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<Bug> Bugs => Set<Bug>();
        public DbSet<Attachment> Attachments => Set<Attachment>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<BugUser> BugUsers => Set<BugUser>();
        //public DbSet<User> Users => Set<User>();
        //public DbSet<CustomUser> CustomUsers => Set<CustomUser>();

    }
}
